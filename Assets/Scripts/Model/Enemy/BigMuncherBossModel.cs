using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class BigMuncherBossModel : EnemyModel
{

    public Animator m_Animator;

    SkinnedMeshRenderer m_Renderer;
    public GameObject rendererHolder;
    float dissolvedPercentage = 0f;

    public GameObject groundCrack;

    public GameObject lightning;

    BigMuncherController bigMuncherController;
    override public void Start() {
        base.Start();

        health = 1f * gameManager.getDifficulty();
        if (lifeMultiplier != -1) {
          health *= lifeMultiplier;
        }

        maxHealth = health;
        healthSlider.value = 1;

        // m_Renderer = rendererHolder.GetComponentInChildren<SkinnedMeshRenderer>();
        bigMuncherController = gameObject.GetComponent<BigMuncherController>();
    }

    void Update() {
        ManageAnimations();
    }

    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        Debug.Log("Big Muncher was hit! Health: " + health);
    }

    public void ManageAnimations() {
      if (bigMuncherController.isRunning()) {
        m_Animator.SetBool("isRunning", true);
      } else {
        m_Animator.SetBool("isRunning", false);
      }
    }

    public void DeadAnimation() {
      dissolvedPercentage = dissolvedPercentage + 0.01f;
      m_Renderer.materials[0].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
    }

    override public void Die() {
        m_Animator.SetTrigger("die");
        dead = true;
        bigMuncherController.StopMovement();

        if (spawnManager != null){
            spawnManager.enemyDied(this.gameObject);
        }

        StartCoroutine("AfterDeath");

        // StartCoroutine(Dissolve());
    }

    IEnumerator AfterDeath() {
      yield return new WaitForSeconds(7f);
      LaunchLightning();
      yield return new WaitForSeconds(2f);
      OpenCrack();
      Destroy(gameObject);
    }

    void LaunchLightning() {
      lightning.SetActive(true);
    }

    void OpenCrack() {
        Vector3 crackPosition = transform.position;
        crackPosition.y -= 1;
        GameObject crack = Instantiate(groundCrack, crackPosition, transform.rotation);
    }

    public IEnumerator DieDelay() {
        yield return new WaitForSeconds(2);

        if (gameManager != null) {
          gameManager.addEnemyKilled();
        }

        Destroy(gameObject);
    }

    public IEnumerator Dissolve() {
      yield return new WaitForSeconds(1);
      InvokeRepeating("DeadAnimation", 0f, 0.01f);

      StartCoroutine(DieDelay());
    }
}
