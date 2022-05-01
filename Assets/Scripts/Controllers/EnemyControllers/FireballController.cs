using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float fireballAttackDamage = 2f;
    public float fireballAttackDamageArea = 3f;

    public Vector3 finalPosition = Vector3.positiveInfinity;

    public LayerMask layerMask;

    PlayerManager playerManager;
    Transform player;
    Enemy2Controller enemyController;

    float xDir = 0;
    float yDir = 1;
    float maxHeight = 10;
    float speed = 0.1f;

    delegate void Direction();
    Direction direction = null;

    void Start()
    {
        playerManager = PlayerManager.instance;
        enemyController = GetComponent<Enemy2Controller>();
        direction = UP;
    }

    void FixedUpdate()
    {
        if (MovementDefined()) {
            Move();
        }
    }

    bool MovementDefined() { 
        return finalPosition != Vector3.positiveInfinity;
    }

    void Move() {
        direction();
    }

    bool reachedTarget() {
        return transform.position.x == finalPosition.x && transform.position.z == finalPosition.z;
    }

    void UP() {
        float y = transform.position.y + 1 * speed;
        if (y >= maxHeight) {
            y = maxHeight;
            direction = HORIZONTAL;
        }

        Vector3 position = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = position;
    }

    void HORIZONTAL() {
        float dirX = (transform.position.x - finalPosition.x) / Mathf.Abs(transform.position.x - finalPosition.x);
        float dirZ = (transform.position.z - finalPosition.z) / Mathf.Abs(transform.position.z - finalPosition.z);
        float x = transform.position.x + dirX * speed;
        float z = transform.position.z + dirZ * speed;
        if (ExceededX(x, finalPosition.x, dirX) && ExceededZ(z, finalPosition.z, dirZ)) {
            x = finalPosition.x;
            z = finalPosition.z;
            direction = DOWN;
        }
        else if (ExceededX(x, finalPosition.x, dirX)) {
            x = finalPosition.x;
        }
        else if (ExceededZ(z, finalPosition.z, dirZ)) {
            z = finalPosition.z;
        }

        Vector3 position = new Vector3(x, transform.position.y, z);
        transform.position = position;
    }

    bool ExceededX(float curX, float finalX, float dirX) {
        return (curX >= finalX && dirX == 1) || (curX <= finalX && dirX == -1);
    }

    bool ExceededZ(float curZ, float finalZ, float dirZ) {
        return (curZ >= finalZ && dirZ == 1) || (curZ <= finalZ && dirZ == -1);
    }

    void DOWN() {
        float y = transform.position.y - 1 * speed;
        Vector3 position = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = position;
    }

    void OnTriggerEnter(Collider other) {
        // foreach (int layer in layerMask) {
        //     if (other.gameObject.layer == layer) {
        //         Debug.Log("Ground was hit!");
        //         DoDamage();
        //         enemyController.ResetNextAttack();
        //     }
        // }
        // Debug.Log(layerMask);
        DoDamage();
    }

    void DoDamage() {
        // Vector3 impactPoint = transform.position;
        // bool playerHit = playerManager.PlayerWithinArea(impactPoint, fireballAttackDamageArea);
        // if (playerHit) {
        //     Debug.Log("Player was Hit!");
        //     playerManager.TakeDamage(fireballAttackDamage);
        // }
        Debug.Log("Collision*****************");
        Destroy(this.gameObject);
    }
}
