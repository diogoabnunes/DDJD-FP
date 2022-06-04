using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeImpactPoint : MonoBehaviour
{
    public LayerMask playerLayer;

    void OnTriggerEnter(Collider other) {
        ChargeAttack chargeAttack = transform.parent.gameObject.GetComponent<ChargeAttack>();

        Debug.Log(other.gameObject.name);

        if (EnemyCollidedWithPlayer(other)) {
            chargeAttack.EnemyCollidedWithPlayer();
        }
        else if (EnemyReachedArenaLimits(other)) {
            chargeAttack.EnemyCollidedWithEndOfArena();
        }
        else {
            chargeAttack.EnemyCollidedWithObject();
        }
    }

    bool EnemyCollidedWithPlayer(Collider other) {
        return (playerLayer.value & (1 << other.gameObject.layer)) > 0;
    }

    bool EnemyReachedArenaLimits(Collider other) {
        return other.gameObject.name == "ArenaLimits";
    }
}
