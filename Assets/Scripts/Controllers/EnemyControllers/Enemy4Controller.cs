using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy4Attack))]
public class Enemy4Controller : EnemyController
{
    Enemy4Attack enemy4Attack;

    void Start() {
        base.Start();
        enemy4Attack = GetComponent<Enemy4Attack>();
    }

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return enemy4Attack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    public override void Attack() {
        Debug.Log("Enemy 4 Attack");
        
        StartCoroutine(enemy4Attack.DoAttack());
    }
}
