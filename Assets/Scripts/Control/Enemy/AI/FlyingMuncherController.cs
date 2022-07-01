using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingMuncherAttack))]
public class FlyingMuncherController : EnemyController
{
    public float stoppingDistance = 5f;
    bool previousActionWasRandomMovement = false;

    FlyingMuncherAttack flyingMuncherAttack;

    override public void Start() {
        base.Start();
        flyingMuncherAttack = GetComponent<FlyingMuncherAttack>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) return GetActionWhenEnemyCanAttack();
        if (TooCloseOfPlayer(distanceToPlayer)) return GetActionWhenEnemyIsTooCloseFromPlayer();
        if (PlayerInLookRange(distanceToPlayer)) return GetActionWhenPlayerIsInLookRange(rotationTowardsPlayer);

        return null;
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        //
        //
        return flyingMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    bool TooCloseOfPlayer(float distanceToPlayer) {
        return distanceToPlayer <= stoppingDistance;
    }

    Action GetActionWhenEnemyIsTooCloseFromPlayer() {
        if (previousActionWasRandomMovement && !IsStopped()) return null;
        
        previousActionWasRandomMovement = true;

        return new RandomMovementAction(base.gameObject);
    }

    Action GetActionWhenEnemyCanAttack() {
        previousActionWasRandomMovement = false;
        return new AttackAction(flyingMuncherAttack);
    }

    Action GetActionWhenPlayerIsInLookRange(Quaternion rotationTowardsPlayer) {
        previousActionWasRandomMovement = false;
        //
        return new ChaseAction(base.gameObject, rotationTowardsPlayer);
    }
}
