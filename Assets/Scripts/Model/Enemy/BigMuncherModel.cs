using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMuncherModel : EnemyModel
{

    public float health;

    GameManager gameManager;
    void Awake() {
        gameManager = GameManager.instance;
    }

    override public void Start() {
        base.Start();

        health = 10f * gameManager.getDifficulty();
    }


    override public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Big Muncher was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
    }

    override public void Die() {
        base.Die();
        Debug.Log("Big Muncher died!");
    }
}
