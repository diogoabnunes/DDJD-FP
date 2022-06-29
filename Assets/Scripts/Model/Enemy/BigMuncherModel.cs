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

    override public void Die() {
        base.Die();
        Debug.Log("Big Muncher died!");
    }
}
