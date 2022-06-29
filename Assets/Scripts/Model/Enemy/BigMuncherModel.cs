using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMuncherModel : EnemyModel
{

    public Animator m_Animator;

    SkinnedMeshRenderer m_Renderer;
    public GameObject rendererHolder;
    float dissolvedPercentage = 0f;


    BigMuncherController bigMuncherController;
    override public void Start() {
        base.Start();

        health = 100f * gameManager.getDifficulty();
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

        StartCoroutine(Dissolve());

        Debug.Log("Colossal Muncher died!");
    }

    public IEnumerator DieDelay() {
        yield return new WaitForSeconds(2);

        if (gameManager != null) {
          gameManager.addEnemyKilled();
        }

        Debug.Log("AAAAAAAAAAAAAAAAAAAA");
        Destroy(gameObject);
    }

    public IEnumerator Dissolve() {
      yield return new WaitForSeconds(1);
      InvokeRepeating("DeadAnimation", 0f, 0.01f);

      StartCoroutine(DieDelay());
    }
}
