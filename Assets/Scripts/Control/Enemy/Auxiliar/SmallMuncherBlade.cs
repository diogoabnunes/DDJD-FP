using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMuncherBlade : MonoBehaviour
{
    public float damage;

    List<GameObject> hittenObjects = new List<GameObject>();
    bool active = false;

    void OnEnable() {
        active = true;
    }

    void OnDisable() {
        active = false;
        hittenObjects.Clear();
    } 

    void OnTriggerEnter(Collider other) {
        if (!active) return;
        
        if (ObjectAlreadyHittenInThisAttack(other)) return;

        CharacterModel model = GetModel(other);
        if (model != null) {
            new TakeDamage(damage, model).execute();
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

        return null;
    }
}
