using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCommand : Command
{
    Vector3 direction;

    public RunCommand(Vector3 direction) {
        this.direction = direction;
    }

    public override void execute(PlayerController playerController) {
        playerController.Run(direction);
    }
}
