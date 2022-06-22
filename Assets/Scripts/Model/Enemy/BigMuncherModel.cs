using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMuncherModel : EnemyModel
{

    override public void Start() {
        base.Start();

        health = 10f * gameManager.getDifficulty();
        maxHealth = health;
        healthSlider.value = 1;
    }


    override public void TakeDamage(float damage) {
        base.TakeDamage(damage);
        Debug.Log("Big Muncher was hit! Health: " + health);
    }

    override public void Die() {
        base.Die();
        Debug.Log("Big Muncher died!");
    }
}
