using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability3Command : Command
{
    public override void execute(PlayerController playerController) {
        Debug.Log("Ability 3 Command");

        playerController.GetActiveWeapon().Ability3();
    }
}
