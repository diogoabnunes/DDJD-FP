using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletransportManager : MonoBehaviour
{
    public float PERCENTAGE_OF_DAMAGE = 0.3f;

    public Transform teletransportPositions;
    List<Transform> positions = new List<Transform>();
    
    InteractionManager interactionManager;

    void Start() {
        interactionManager = InteractionManager.instance;

        foreach (Transform child in teletransportPositions) {
            positions.Add(child);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name != "Third Person Player") {
            return;
        }

        GameObject player = other.gameObject;
        Teletransport(player);
    }

    void Teletransport(GameObject player) {
        PlayerModel playerModel = player.GetComponent<PlayerModel>();

        ApplyDamageToPlayer(playerModel);
        TeletransportPlayer(player, playerModel);
    }

    void ApplyDamageToPlayer(PlayerModel playerModel) {
        float playerMaxHealth = playerModel.GetMaxHealth();
        float damage = playerMaxHealth * PERCENTAGE_OF_DAMAGE;
        
        interactionManager.manageInteraction(new TakeDamage(damage, playerModel));
    }

    void TeletransportPlayer(GameObject player, PlayerModel playerModel) {
        if (playerModel.IsDead()) {
            return;
        }

        int index = Random.Range(0, positions.Count);
        Transform selectedPosition = positions[index];
        // player.GetComponent<PlayerController>().Disable();
        player.transform.position = selectedPosition.position;
    }
}
