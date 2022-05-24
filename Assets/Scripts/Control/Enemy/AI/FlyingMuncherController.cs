using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingMuncherAttack))]
public class FlyingMuncherController : EnemyController
{
    FlyingMuncherAttack flyingMuncherAttack;

    void Start() {
        base.Start();
        flyingMuncherAttack = GetComponent<FlyingMuncherAttack>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) return new AttackAction(flyingMuncherAttack);
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);

        return null;
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return flyingMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }
}
