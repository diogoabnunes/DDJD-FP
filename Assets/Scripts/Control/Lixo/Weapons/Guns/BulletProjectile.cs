using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Rigidbody bulletRigidBody;

    public float speed = 5f;
    public float damage = 3f;

    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        bulletRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        EnemyController controller = other.gameObject.GetComponent<EnemyController>();
        if (controller != null) {
            controller.TakeDamage(damage);
        }

        Debug.Log(other.gameObject.name);

        Destroy(gameObject);
    }
}
