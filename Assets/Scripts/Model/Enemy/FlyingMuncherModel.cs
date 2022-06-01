using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMuncherModel : EnemyModel
{

    public float health;

    GameManager gameManager;
    void Awake() {
        gameManager = GameManager.instance;
    }

    private void Start() {
        health = 1f * gameManager.getDifficulty();
    }


    override public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Flying Muncher was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Flying Muncher died!");
    }
}