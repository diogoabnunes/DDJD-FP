using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopMovementAction : Action
{
    EnemyController controller;

    public StopMovementAction(GameObject obj) {
        this.controller = obj.GetComponent<EnemyController>();
    }

    public override void execute() {
        controller.CancelMovement();
    }
}
