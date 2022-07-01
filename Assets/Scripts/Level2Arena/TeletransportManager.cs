using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletransportManager : MonoBehaviour
{
    public float PERCENTAGE_OF_DAMAGE = 0.3f;

    public Transform teletransportPositions;
    List<Transform> positions = new List<Transform>();
    
    InteractionManager interactionManager;

    GameObject player;

    void Start() {
        interactionManager = InteractionManager.instance;

        foreach (Transform child in teletransportPositions) {
            positions.Add(child);
        }

        player = GameObject.Find("Third Person Player");
    }

    void Update() {
        if (player.transform.position.y <= transform.position.y) {
            Teletransport();
        }
    }

    void Teletransport() {
        PlayerModel playerModel = player.GetComponent<PlayerModel>();

        ApplyDamageToPlayer(playerModel);
        TeletransportPlayer(playerModel);
    }

    void ApplyDamageToPlayer(PlayerModel playerModel) {
        float playerMaxHealth = playerModel.GetMaxHealth(); 
        float damage = playerMaxHealth * PERCENTAGE_OF_DAMAGE;
        
        
        interactionManager.manageInteraction(new TakeDamage(damage, playerModel, false));
    }

    void TeletransportPlayer(PlayerModel playerModel) {
        if (playerModel.IsDead()) {
            // 
            return;
        }

        

        int index = Random.Range(0, positions.Count);
        Transform selectedPosition = positions[index];
        player.GetComponent<PlayerController>().GoTo(selectedPosition.position);
    }
}
