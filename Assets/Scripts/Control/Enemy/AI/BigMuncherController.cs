using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PunchGroundAttack))]
[RequireComponent(typeof(FireballAttack))]
public class BigMuncherController : EnemyController
{
    PunchGroundAttack punchGroundAttack;
    FireballAttack fireballAttack;

    void Start() {
        base.Start();
        punchGroundAttack = GetComponent<PunchGroundAttack>();
        fireballAttack = GetComponent<FireballAttack>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (punchGroundAttack.CanAttack(distanceToPlayer)) return new AttackAction(punchGroundAttack);
        if (fireballAttack.CanAttack(distanceToPlayer)) return new AttackAction(fireballAttack);
        if (PlayerInLookRange(distanceToPlayer)) return new MoveToAction(agent, GetPlayerPosition());

        return null;
    }
}
