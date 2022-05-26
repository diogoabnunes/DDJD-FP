using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackCommand : Command
{
    public override void execute(PlayerController playerController) {
        Debug.Log("Basic attack Command");

        playerController.GetActiveWeapon().BasicAttack();
    }
}
