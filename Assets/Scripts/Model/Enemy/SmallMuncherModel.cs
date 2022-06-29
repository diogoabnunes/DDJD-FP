using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallMuncherModel : EnemyModel
{
    public Animator m_Animator;
    SkinnedMeshRenderer m_Renderer;
    public GameObject rendererHolder;
    float dissolvedPercentage = 0f;


    SmallMuncherController smallMuncherController;

    override public void Start() {

        base.Start();

        health = 2f * gameManager.getDifficulty();
        if (lifeMultiplier != -1) {
          health *= lifeMultiplier;
        }

        maxHealth = health;
        healthSlider.value = 1;

        m_Renderer = rendererHolder.GetComponentInChildren<SkinnedMeshRenderer>();
        smallMuncherController = gameObject.GetComponent<SmallMuncherController>();
    }

    void Update() {
        ManageAnimations();
    }

    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        Debug.Log("Small Muncher was hit " + damage +"! Health: " + health);
    }

    public void ManageAnimations() {
      if (smallMuncherController.isRunning()) {
        m_Animator.SetBool("isRunning", true);
      } else {
        m_Animator.SetBool("isRunning", false);
      }
    }

    public void DeadAnimation() {
      dissolvedPercentage = dissolvedPercentage + 0.01f;
      m_Renderer.materials[0].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
      m_Renderer.materials[1].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
    }

    override public void Die() {
        m_Animator.SetTrigger("die");
        dead = true;
        smallMuncherController.StopMovement();
        StartCoroutine(Dissolve());

        Debug.Log("Small Muncher died!");
    }

    public IEnumerator DieDelay() {
        if (spawnManager != null){
            spawnManager.enemyDied(this.gameObject);
        }

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
