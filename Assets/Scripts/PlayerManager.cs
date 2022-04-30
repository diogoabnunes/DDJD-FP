using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    #region Singleton

    public static PlayerManager instance;

    void Awake() {
        instance = this;
    }
    
    #endregion

    public GameObject player;

    public float health = 10f;

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Player was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Player died!");
    }
}
