using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMuncherModel : EnemyModel
{

    public float health;

    GameManager gameManager;
    void Awake() {
        gameManager = GameManager.instance;
    }

    private void Start() {
        health = 2f * gameManager.getDifficulty();
    }


    override public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Small Muncher was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Small Muncher died!");
    }
}