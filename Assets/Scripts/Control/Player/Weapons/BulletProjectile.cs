using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Rigidbody bulletRigidBody;

    public float speed = 80f;
    public float damage = 3f;

    private InteractionManager interactionManager;

    void Awake()
    {
        interactionManager = InteractionManager.instance;
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        bulletRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {

        CharacterModel model = other.gameObject.GetComponent<CharacterModel>();
        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(damage, model));
        }

        Destroy(gameObject);
    }
}
