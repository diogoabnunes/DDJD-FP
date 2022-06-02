using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseAction : Action
{
    EnemyController controller;
    Quaternion rotation;

    public ChaseAction(GameObject obj, Quaternion rotation) {
        this.controller = obj.GetComponent<EnemyController>();
        this.rotation = rotation;
    }

    public override void execute() {
        controller.ChasePlayer();
        // controller.FacePlayer(rotation);
    }
}
