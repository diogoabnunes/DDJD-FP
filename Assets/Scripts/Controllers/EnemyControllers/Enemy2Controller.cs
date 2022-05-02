using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PunchGroundAttack))]
[RequireComponent(typeof(FireballAttack))]
public class Enemy2Controller : EnemyController
{
    // DEBUG
    public GameObject damageAreaObject;

    PunchGroundAttack punchGroundAttack;
    FireballAttack fireballAttack;

    delegate IEnumerator NextAttack();
    NextAttack nextAttack = null;

    void Start() {
        base.Start();
        punchGroundAttack = GetComponent<PunchGroundAttack>();
        fireballAttack = GetComponent<FireballAttack>();
    }

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return InPunchGroundAttackRange(distanceToPlayer) || InFireballAttackRange(distanceToPlayer);
    }

    bool InPunchGroundAttackRange(float distanceToPlayer) {
        bool canAttack = punchGroundAttack.CanAttack(distanceToPlayer);
        
        if (canAttack) {
            nextAttack = punchGroundAttack.DoAttack;

            Debug.Log("Picked Next Attack as: PunchGround");
        }

        return canAttack;
    }

    bool InFireballAttackRange(float distanceToPlayer) {
        bool canAttack = fireballAttack.CanAttack(distanceToPlayer);
        
        if (canAttack) {
            nextAttack = fireballAttack.DoAttack;

            Debug.Log("Picked Next Attack as: Fireball");
        }

        return canAttack;
    }

    public override void Attack() {
        Debug.Log("Enemy 2 Attack");

        if (nextAttack != null) {
            StartCoroutine(nextAttack());
        }
        else {
            Debug.Log("NextAttack was not defined!");
        }
    }

    public override void AttackEnded() {
        nextAttack = null;
        isAttacking = false;

        Debug.Log("Next Attack Reseted");
    }

    // DEBUG
    public void DrawDamageArea(Vector3 impactPoint, float damageArea) {
        GameObject obj = Instantiate(damageAreaObject, impactPoint, Quaternion.identity);
        obj.transform.position = new Vector3(obj.transform.position.x, -8, obj.transform.position.z); 
        obj.transform.localScale *= damageArea;
        obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y / damageArea, obj.transform.localScale.z);
    }
}
