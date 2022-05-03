using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy4Controller))]
public class Enemy4Attack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;

    Enemy4Controller enemy4Controller;
    PlayerManager playerManager;

    void Start() {
        enemy4Controller = GetComponent<Enemy4Controller>();
        playerManager = PlayerManager.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range;
    }

    public override IEnumerator DoAttack() {
        Debug.Log("Enemy 4 Attack");

        enemy4Controller.AttackStarted();

        enemy4Controller.StopMovement();

        // play animation of attack
        // verify if player is still in range
        playerManager.TakeDamage(damage);

        yield return new WaitForSeconds(duration);

        enemy4Controller.AttackEnded();

        yield return null;
    }
}
