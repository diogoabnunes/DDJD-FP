using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3AcidPool : MonoBehaviour
{
    public GameObject player;
    PlayerModel playerModel;

    InteractionManager interactionManager;

    bool playerInAcidPool = false;
    float initialDamageToPlayer = 0.1f;
    float currentDamageToPlayer;
    float damageIncrease = 0.1f;

    float nextDamageTime = 0f;
    float damageInterval = 1f;

    void Start() {
        playerModel = player.GetComponent<PlayerModel>();

        interactionManager = InteractionManager.instance;
        
        currentDamageToPlayer = initialDamageToPlayer;
    }
    
    void Update() {
        if (CanDealDamageToPlayer()) {
            DealDamage();
        }
    }

    bool CanDealDamageToPlayer() {
        return playerInAcidPool && TimeElapsedSinceLastDamage();
    }

    bool TimeElapsedSinceLastDamage() {
        return nextDamageTime <= Time.time;
    }

    void DealDamage() {
        DealDamageToPlayer();
        SetupNextDamage();
    }

    void DealDamageToPlayer() {
        interactionManager.manageInteraction(new TakeDamage(currentDamageToPlayer, playerModel));
    }

    void SetupNextDamage() {
        currentDamageToPlayer += damageIncrease;
        nextDamageTime = Time.time + damageInterval;
    }

    void PlayerLeftAcidPool() {
        playerInAcidPool = false;
        currentDamageToPlayer = initialDamageToPlayer;
        nextDamageTime = Time.time;
    }

    void OnTriggerEnter(Collider other) {
        if (PlayerInAcidPool(other.gameObject.name)) {
            playerInAcidPool = true;
        }
    }

    bool PlayerInAcidPool(string name) {
        return name == "Third Person Player";
    }
}
