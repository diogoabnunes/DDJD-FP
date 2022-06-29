using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingMuncherModel : EnemyModel
{
    public Animator m_Animator;

    SkinnedMeshRenderer m_Renderer;
    public GameObject rendererHolder;
    float dissolvedPercentage = 0f;

    FlyingMuncherController flyingMuncherController;

    override public void Start() {
        base.Start();

        health = 1.5f * gameManager.getDifficulty();
        if (lifeMultiplier != -1) {
          health *= lifeMultiplier;
        }
        
        maxHealth = health;
        healthSlider.value = 1;

        m_Renderer = rendererHolder.GetComponentInChildren<SkinnedMeshRenderer>();
        flyingMuncherController = gameObject.GetComponent<FlyingMuncherController>();
    }

    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        Debug.Log("Flying Muncher was hit for " + damage +"! Health: " + health);
    }

    public void ManageAnimations() {
      if (flyingMuncherController.isRunning()) {
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
        flyingMuncherController.StopMovement();

        if (spawnManager != null){
            spawnManager.enemyDied(this.gameObject);
        }

        StartCoroutine(Dissolve());

        Debug.Log("Small Muncher died!");
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
