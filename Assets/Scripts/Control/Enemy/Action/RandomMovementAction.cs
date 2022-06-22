using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovementAction : Action
{
    EnemyController controller;

    float farthestDistance = 50f;

    public RandomMovementAction(GameObject obj) {
        this.controller = obj.GetComponent<EnemyController>();
    }

    public override void execute() {
        Vector3 currentPosition = controller.GetEnemyPosition();
        Vector3 playerPosition = controller.GetPlayerPosition();
        Vector3 direction = (playerPosition - currentPosition).normalized;

        float newX = direction.x;
        if (newX < 0) newX -= farthestDistance;
        else newX += farthestDistance;

        float newY = currentPosition.y;

        float newZ = direction.z;
        if (newZ < 0) newZ -= farthestDistance;
        else newZ += farthestDistance;

        Vector3 newPosition = new Vector3(newX, newY, newZ);

        controller.GoTo(newPosition);
    }
}
