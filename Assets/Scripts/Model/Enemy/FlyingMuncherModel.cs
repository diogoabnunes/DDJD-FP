using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMuncherModel : EnemyModel
{

    public float health;

    public Animator m_Animator;

    SkinnedMeshRenderer m_Renderer;
    public GameObject rendererHolder;
    float dissolvedPercentage = 0f;

    FlyingMuncherController flyingMuncherController;

    override public void Start() {
        base.Start();

        health = 1f * gameManager.getDifficulty();
        m_Renderer = rendererHolder.GetComponentInChildren<SkinnedMeshRenderer>();
        flyingMuncherController = gameObject.GetComponent<FlyingMuncherController>();
    }

    override public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Small Muncher was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
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

        Destroy(gameObject);
    }

    public IEnumerator Dissolve() {
      yield return new WaitForSeconds(1);
      InvokeRepeating("DeadAnimation", 0f, 0.01f);

      StartCoroutine(DieDelay());
    }
}
