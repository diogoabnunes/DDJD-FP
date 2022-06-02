using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfArenaCollider : MonoBehaviour
{
    float VERY_BIG_DAMAGE = 1000f;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Third Person Player") {
            PlayerModel[] playerModel = FindObjectsOfType<PlayerModel>();
            if (playerModel.Length != 0) {
                playerModel[0].TakeDamage(VERY_BIG_DAMAGE);
            }
        }

        CharacterModel model = other.gameObject.GetComponent<CharacterModel>();
        if (model != null) {
            model.TakeDamage(VERY_BIG_DAMAGE);
        }
    }
}
