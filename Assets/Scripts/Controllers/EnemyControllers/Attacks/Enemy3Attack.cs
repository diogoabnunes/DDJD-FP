using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy3Controller))]
public class Enemy3Attack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;

    Enemy3Controller enemy3Controller;
    PlayerManager playerManager;

    void Start() {
        enemy3Controller = GetComponent<Enemy3Controller>();
        playerManager = PlayerManager.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range;
    }

    public override IEnumerator DoAttack() {
        Debug.Log("Enemy 3 Attack");

        enemy3Controller.AttackStarted();

        enemy3Controller.StopMovement();

        // play animation of attack
        // verify if player is still in range
        playerManager.TakeDamage(damage);

        yield return new WaitForSeconds(duration);

        enemy3Controller.AttackEnded();

        yield return null;
    }
}
