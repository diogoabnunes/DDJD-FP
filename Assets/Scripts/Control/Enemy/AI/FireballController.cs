using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float damage = 2f;
    public float damageArea = 3f;

    public LayerMask layerMask;

    public float maxHeight = 20f;
    public float gravity = -18f;

    Vector3 target;

    PlayerManager playerManager;
    BigMuncherController enemyController;
    Rigidbody rigidBody;

    void Awake()
    {
        playerManager = PlayerManager.instance;
        enemyController = GetComponent<BigMuncherController>();
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.useGravity = false;
    }

    void Start() {}

    public void SetTarget(Vector3 _target) {
        target = _target;
        InitiateThrow();
    }

    void InitiateThrow() {
        Vector3 velocity = ComputeVelocity();
        Throw(velocity);
    }

    // youtube: https://www.youtube.com/watch?v=IvT8hjy6q4o
    Vector3 ComputeVelocity() {
        float displacementY = target.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

        float timeOfFlight = Mathf.Sqrt(-2 * maxHeight / gravity) + Mathf.Sqrt(2 * (displacementY - maxHeight) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * maxHeight * gravity);
        Vector3 velocityXZ = displacementXZ / timeOfFlight;

        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }

    void Throw(Vector3 velocity) {
        Physics.gravity = Vector3.up * gravity;
        rigidBody.useGravity = true;

        rigidBody.velocity = velocity;
    }

    void OnTriggerEnter(Collider other) {
        if (HitSomething(other.gameObject.layer)) {
            DoDamage();
            Destroy(this.gameObject);
        }
    }

    bool HitSomething(int objectLayer) {
        return (layerMask & 1 << objectLayer) != 0;
    }

    void DoDamage() {
        Vector3 impactPoint = transform.position;
        bool playerHit = playerManager.PlayerWithinArea(impactPoint, damageArea);
        if (playerHit) {
            Debug.Log("Player was Hit!");
            playerManager.TakeDamage(damage);
        }
    }
}
