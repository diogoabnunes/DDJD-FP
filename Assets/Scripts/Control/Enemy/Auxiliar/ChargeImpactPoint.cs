using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeImpactPoint : MonoBehaviour
{
    public LayerMask playerLayer;

    Vector3 arenaCenterPoint;
    float arenaRadius;

    void Awake() {
        arenaCenterPoint = Vector3.zero;
        arenaRadius = 100f;
    }

    void Update() {
        float distanceToCenter = Vector3.Distance(transform.position, arenaCenterPoint);
        if (Mathf.Abs(distanceToCenter - arenaRadius) <= 2f) {
            ChargeAttack chargeAttack = transform.parent.gameObject.GetComponent<ChargeAttack>();
            chargeAttack.EnemyCollidedWithEndOfArena();
        }
    }

    void OnTriggerEnter(Collider other) {
        ChargeAttack chargeAttack = transform.parent.gameObject.GetComponent<ChargeAttack>();

        Debug.Log(other.gameObject.name);

        if (EnemyCollidedWithPlayer(other)) {
            chargeAttack.EnemyCollidedWithPlayer();
        }
        else {
            chargeAttack.EnemyCollidedWithObject();
        }
    }

    bool EnemyCollidedWithPlayer(Collider other) {
        return (playerLayer.value & (1 << other.gameObject.layer)) > 0;
    }
}
