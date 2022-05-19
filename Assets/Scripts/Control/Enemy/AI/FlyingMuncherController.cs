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

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return flyingMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    public override void Attack() {
        Debug.Log("Flying Muncher Attack");
        
        StartCoroutine(flyingMuncherAttack.DoAttack());
    }
}
