using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    Attack attack;

    public AttackAction(Attack attack) {
        this.attack = attack;
    }

    public override void execute() {
        attack.DoAttack();
    }
}
