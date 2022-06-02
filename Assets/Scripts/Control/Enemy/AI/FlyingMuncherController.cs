using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingMuncherAttack))]
public class FlyingMuncherController : EnemyController
{
    public float stoppingDistance = 5f;

    FlyingMuncherAttack flyingMuncherAttack;

    override public void Start() {
        base.Start();
        flyingMuncherAttack = GetComponent<FlyingMuncherAttack>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) return new AttackAction(flyingMuncherAttack);
        if (TooCloseOfPlayer(distanceToPlayer)) return new StopMovementAction(base.gameObject);
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);

        return null;
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return flyingMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    bool TooCloseOfPlayer(float distanceToPlayer) {
        return distanceToPlayer <= stoppingDistance;
    }
}
