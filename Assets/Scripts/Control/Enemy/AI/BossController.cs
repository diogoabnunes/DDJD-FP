using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    void Start()
    {
        base.Start();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (PlayerInLookRange(distanceToPlayer)) return new MoveToAction(agent, GetPlayerPosition());

        return null;
    }
}
