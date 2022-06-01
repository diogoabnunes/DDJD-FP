using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : CharacterModel
{

    public GameObject player;

    public float health = 10f;

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
    }

    #endregion


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
        Debug.Log("Player was hit! Health: " + health);

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Player died!");
    }
}
