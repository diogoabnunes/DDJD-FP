using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Rigidbody bulletRigidBody;

    public float speed = 80f;
    public float damage = 3f;
    public Vector3 direction = Vector3.zero;

    private InteractionManager interactionManager;

    void Awake()
    {
        interactionManager = InteractionManager.instance;
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {   
        bulletRigidBody.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Spawner")){
            return;
        }

        CharacterModel model = collider.gameObject.GetComponent<CharacterModel>();
        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(damage, model));
    
            Destroy(gameObject);
        }
    }
}
