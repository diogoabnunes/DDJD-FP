using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : EnemyModel
{
    public float health;

    override public void Start() {
        base.Start();

        health = 25f * gameManager.getDifficulty();
    }

    override public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Boss was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
    }

    override public void Die() {
        base.Die();
        Debug.Log("Boss died!");
    }
}
