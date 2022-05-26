using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackCommand : Command
{
    public override void execute(PlayerController playerController) {
        WeaponController weapon = playerController.GetActiveWeapon();
        if (weapon != null) weapon.BasicAttack();
    }
}
