using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PunchGroundAttack))]
[RequireComponent(typeof(BigMuncherFireball))]
public class BigMuncherController : EnemyController
{

    PunchGroundAttack punchGroundAttack;
    BigMuncherFireball fireballAttack;

    override public void Start() {
        base.Start();
        punchGroundAttack = GetComponent<PunchGroundAttack>();
        fireballAttack = GetComponent<BigMuncherFireball>();
        
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (punchGroundAttack.CanAttack(distanceToPlayer)) return new AttackAction(punchGroundAttack);
        if (fireballAttack.CanAttack(distanceToPlayer)) return new AttackAction(fireballAttack);
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);

        return null;
    }
}
