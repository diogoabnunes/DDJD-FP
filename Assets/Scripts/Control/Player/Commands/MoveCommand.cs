using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    Vector3 direction;

    public MoveCommand(Vector3 direction) {
        this.direction = direction;
    }

    public override void execute(PlayerController playerController) {
        playerController.Move(direction, -1);
    }
}
