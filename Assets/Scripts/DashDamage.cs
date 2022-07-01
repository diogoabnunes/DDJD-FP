using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    public float damage;

    InteractionManager interactionManager;

    void Start()
    {
        interactionManager = InteractionManager.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterModel model = other.gameObject.GetComponent<CharacterModel>();
        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(damage, model));
        }
    }
}
