using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfArenaCollider : MonoBehaviour
{
    float VERY_BIG_DAMAGE = 1000f;

    void OnTriggerEnter(Collider other) {
        CharacterModel model = other.gameObject.GetComponent<CharacterModel>();
        Debug.Log(model);
        if (model != null) {
            model.TakeDamage(VERY_BIG_DAMAGE);
        }
    }
}
