using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClawAttack))]
[RequireComponent(typeof(FireBreathAttack))]
[RequireComponent(typeof(ChargeAttack))]
public class BossController : EnemyController
{
    List<Attack> attacks = new List<Attack>();

    override public void Start()
    {
        base.Start();

        attacks.Add(GetComponent<ClawAttack>());
        // attacks.Add(GetComponent<FireBreathAttack>());
        // attacks.Add(GetComponent<ChargeAttack>());
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        List<Action> possibleActions = GetPossibleActions(distanceToPlayer, rotationTowardsPlayer);

        if (possibleActions.Count == 0) return null;

        return SelectRandomAction(possibleActions);
    }

    List<Action> GetPossibleActions(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        List<Action> possibleActions = new List<Action>();

        foreach (Attack attack in attacks) {
            if (attack.CanAttack(distanceToPlayer)) possibleActions.Add(new AttackAction(attack));
        }

        if (PlayerInLookRange(distanceToPlayer)) possibleActions.Add(new ChaseAction(base.gameObject, rotationTowardsPlayer));

        return possibleActions;
    }

    Action SelectRandomAction(List<Action> actions) {
        int selectedActionIndex = Random.Range(0, actions.Count);
        return actions[selectedActionIndex];
    }
}
