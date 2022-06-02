using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BiteAttack))]
public class BossController : EnemyController
{
    BiteAttack biteAttack;

    override public void Start()
    {
        base.Start();
        biteAttack = GetComponent<BiteAttack>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        // ataque basico
        // if (biteAttack.CanAttack(distanceToPlayer)) return new AttackAction(biteAttack);
        // correr e bater contra o jogador
        // cuspir fogo
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);
        // Debug.Log("No Command");

        return null;
    }
}
