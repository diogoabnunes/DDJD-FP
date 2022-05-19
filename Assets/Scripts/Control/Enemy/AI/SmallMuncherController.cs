using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmallMuncherAttack))]
public class SmallMuncherController : EnemyController
{
    SmallMuncherAttack smallMuncherAttack;

    void Start() {
        base.Start();
        smallMuncherAttack = GetComponent<SmallMuncherAttack>();
    }

    public override bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return smallMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    public override void Attack() {
        Debug.Log("Small Muncher Attack");
        
        StartCoroutine(smallMuncherAttack.DoAttack());
    }
}
