using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    override public void Start()
    {
        base.Start();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);

        return null;
    }
}
