using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCommand : Command
{
    public override void execute(PlayerController playerController) {
        playerController.Idle();
    }
}
