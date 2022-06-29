using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : EnemyModel
{
    override public void Start() {
        base.Start();

        health = 25f * gameManager.getDifficulty();
        if (lifeMultiplier != -1) {
            health *= lifeMultiplier;
        }
        maxHealth = health;
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
