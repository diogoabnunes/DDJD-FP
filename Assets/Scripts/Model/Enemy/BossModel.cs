using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : EnemyModel
{

    public Animator m_Animator;
    BossController bossController;
    override public void Start() {
        base.Start();

        health = 15000f * gameManager.getDifficulty();
        if (lifeMultiplier != -1) {
            health *= lifeMultiplier;
        }
        maxHealth = health;

        bossController = gameObject.GetComponent<BossController>();
    }

    void Update() {
        ManageAnimations();
    }

    public void ManageAnimations() {
      if (bossController.isRunning()) {
        m_Animator.SetBool("isRunning", true);
      } else {
        m_Animator.SetBool("isRunning", false);
      }
    }

    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        Debug.Log("Boss was hit: " + health);
    }

    override public void Die() {
        base.Die();
        Debug.Log("Boss died!");
    }
}
