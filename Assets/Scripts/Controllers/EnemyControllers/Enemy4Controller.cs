using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Controller : EnemyController
{
    public float attackRange = 2.5f;
    public float attackDuration = 3f;
    public float attackDamage = 1f;

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return InAtackRange(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    bool InAtackRange(float distance) {
        return distance <= attackRange;
    }

    public override IEnumerator Attack() {
        Debug.Log("Enemy 4 Attack");

        StopMovement();
        
        isAttacking = true;

        // play animation of attack
        playerManager.TakeDamage(attackDamage);

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }
}
