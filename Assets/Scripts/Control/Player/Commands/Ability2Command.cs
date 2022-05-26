using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2Command : Command
{
    public override void execute(PlayerController playerController) {
        Debug.Log("Ability 2 Command");

        playerController.GetActiveWeapon().Ability2();
    }
}
