using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy3Attack))]
public class Enemy3Controller : EnemyController
{
    Enemy3Attack enemy3Attack;

    void Start() {
        base.Start();
        enemy3Attack = GetComponent<Enemy3Attack>();
    }

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return enemy3Attack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    public override void Attack() {
        Debug.Log("Enemy 3 Attack");
        
        StartCoroutine(enemy3Attack.DoAttack());
    }

    public override int XPGiven() {
        Debug.Log("XP Given: 20");
        return 20;
    }
}
