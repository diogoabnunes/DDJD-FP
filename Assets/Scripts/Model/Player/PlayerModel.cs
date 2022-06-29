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

    public List<OffensiveEffect> offensiveEffects;
    public List<DefensiveEffect> defensiveEffects;
    public List<OtherEffect> otherEffects;

    #region Singleton

    public static PlayerModel instance;

    void Awake() {
        instance = this;
        offensiveEffects  = new List<OffensiveEffect>();
        defensiveEffects  = new List<DefensiveEffect>();
        otherEffects = new List<OtherEffect>();

        InitiateSlider();
    }

    #endregion

    void InitiateSlider() {
        maxHealth = health;
        healthSlider.value = 1;
    }

    public void Reset() {
        health = maxHealth;
        offensiveEffects.Clear();
        defensiveEffects.Clear();
        otherEffects.Clear();
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
        Debug.Log("Player died!");
        GameManager.instance.PlayerDied();
    }
}
