using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : CharacterModel
{

    public GameObject player;
    float maxHealth;
    public float health = 10f;
    public Slider healthSlider;

    public Animator m_Animator;

    #region Singleton

    public static PlayerModel instance;
    public PlayerModifiers playerModifiers;

    void Awake() {
        instance = this;

        playerModifiers = new PlayerModifiers();

        InitiateSlider();
    }

    #endregion

    void InitiateSlider() {
        maxHealth = health;
        healthSlider.value = 1;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public void Reset() {
        health = maxHealth;
        UpdateSlider();
    }

    public void ResetLife() {
        health = maxHealth;
        UpdateSlider();
    }

    public bool PlayerWithinArea(Vector3 position, float radius) {
        Collider[] hitElements = Physics.OverlapSphere(position, radius);

        foreach (Collider hitElement in hitElements) {
            if (hitElement.tag == "Player") {
                return true;
            }
        }

        return false;
    }

    public PlayerModifiers getPlayerModifiers() {
        return playerModifiers;
    }

    override public void TakeDamage(float damage) {
        health -= damage;
        UpdateSlider();

        if (health <= 0) {
            Die();
        }
    }

    void UpdateSlider() {
        healthSlider.value = health / maxHealth;
    }

    void Die() {
        m_Animator.SetTrigger("die");
        GameManager.instance.PlayerDied();
    }

    public bool IsDead() {
        return health <= 0;
    }
}
