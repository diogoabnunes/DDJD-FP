using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : EnemyController
{
    public float punchGroundAttackRange = 5f;
    public float punchGroundAttackDuration = 3f;
    public float punchGroundAttackDamage = 1f;
    public float punchGroundAttackDamageArea = 4f;

    public float minFireballAttackRange = 8f;
    public float maxFireballAttackRange = 10f;
    public float fireballAttackDuration = 3f;
    public float fireballAttackDamage = 2f;
    public float fireballAttackDamageArea = 3f;

    delegate void NextAttack();
    NextAttack nextAttack = null;
    float nextAttackDuration = -1;

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return InAtackRange(distanceToPlayer);
    }

    bool InAtackRange(float distance) {
        return InPunchGroundAttackRange(distance) || InFireballAttackRange(distance);
    }

    bool InPunchGroundAttackRange(float distance) {
        bool canAttack = distance <= punchGroundAttackRange;
        
        if (canAttack) {
            nextAttack = PunchGround;
            nextAttackDuration = punchGroundAttackDuration;

            Debug.Log("Distance: " + distance);
            Debug.Log("Picked Next Attack as: PunchGround");
        }

        return canAttack;
    }

    bool InFireballAttackRange(float distance) {
        bool canAttack = distance >= minFireballAttackRange && distance <= maxFireballAttackRange;
        
        if (canAttack) {
            nextAttack = Fireball;
            nextAttackDuration = fireballAttackDuration;

            Debug.Log("Distance: " + distance);
            Debug.Log("Picked Next Attack as: Fireball");
        }

        return canAttack;
    }

    public override IEnumerator Attack() {
        Debug.Log("Enemy 2 Attack");

        if (nextAttack != null) {
            StopMovement();
            
            isAttacking = true;

            nextAttack();
            yield return new WaitForSeconds(nextAttackDuration);

            ResetNextAttack();
            isAttacking = false;
        }
        else {
            Debug.Log("NextAttack was not defined!");
        }

        yield return null;
    }

    void PunchGround() {
        Debug.Log("Punch Ground Attack!");

        Vector3 impactPoint = transform.position;
        bool playerHit = PlayerWithinArea(impactPoint, punchGroundAttackDamageArea);
        if (playerHit) {
            Debug.Log("Player was Hit!");
            playerManager.TakeDamage(punchGroundAttackDamage);
        }

    }

    void Fireball() {
        Debug.Log("Fireball Attack!");
        
        Vector3 impactPoint = player.position;
        bool playerHit = PlayerWithinArea(impactPoint, fireballAttackDamageArea);
        if (playerHit) {
            Debug.Log("Player was Hit!");
            playerManager.TakeDamage(fireballAttackDamage);
        }
    }

    bool PlayerWithinArea(Vector3 position, float radius) {
        Collider[] hitElements = Physics.OverlapSphere(position, radius);

        foreach (Collider hitElement in hitElements) {
            if (hitElement.tag == "Player") {
                return true;
            }
        }

        return false;
    }

    void ResetNextAttack() {
        nextAttack = null;
        nextAttackDuration = -1;
    }
}
