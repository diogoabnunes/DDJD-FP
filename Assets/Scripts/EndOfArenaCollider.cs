using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfArenaCollider : MonoBehaviour
{
    float VERY_BIG_DAMAGE = 1000f;

    InteractionManager interactionManager;

    public virtual void Start() {
        interactionManager = InteractionManager.instance;
    }

   void OnTriggerEnter(Collider other) {
        CharacterModel model = GetModel(other);
        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(VERY_BIG_DAMAGE, model));
        }
    }

    CharacterModel GetModel(Collider other) {
        if (other.gameObject.name == "Third Person Player") {
            PlayerModel[] playerModel = FindObjectsOfType<PlayerModel>();
            if (playerModel.Length != 0) return playerModel[0];
        }

        return other.gameObject.GetComponent<CharacterModel>();
    }
}
