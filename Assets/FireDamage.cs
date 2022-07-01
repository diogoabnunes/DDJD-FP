using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{

    public float damage;
    InteractionManager interactionManager;
    // Start is called before the first frame update
    void Start()
    {
        interactionManager = InteractionManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        
        CharacterModel model = other.gameObject.GetComponent<CharacterModel>();
        if (model != null && other.tag == "Player") {
            
            interactionManager.manageInteraction(new TakeDamage(damage, model));
        }
    }
}
