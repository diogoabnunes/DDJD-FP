using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1Command : Command
{
    public override void execute(PlayerController playerController) {
        

        playerController.GetActiveWeapon().Ability1();
    }
}
