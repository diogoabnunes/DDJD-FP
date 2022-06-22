using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawImpactPoint : MonoBehaviour
{
    public float damage;

    InteractionManager interactionManager;

    List<GameObject> hittenObjects = new List<GameObject>();

    public virtual void Start() {
        interactionManager = InteractionManager.instance;
    }

    void OnDisable() {
        hittenObjects.Clear();
    } 

    void OnTriggerEnter(Collider other) {
        if (ObjectAlreadyHittenInThisAttack(other)) return;

        CharacterModel model = GetModel(other);
        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(damage, model));
            hittenObjects.Add(other.gameObject);
        }
    }

    bool ObjectAlreadyHittenInThisAttack(Collider other) {
        return hittenObjects.Contains(other.gameObject);
    }

    CharacterModel GetModel(Collider other) {
        if (other.gameObject.name == "Third Person Player") {
            PlayerModel[] playerModel = FindObjectsOfType<PlayerModel>();
            if (playerModel.Length != 0) return playerModel[0];
        }

        return other.gameObject.GetComponent<CharacterModel>();
    }
}
