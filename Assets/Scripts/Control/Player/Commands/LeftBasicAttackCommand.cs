using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBasicAttackCommand : Command
{
    public override void execute(PlayerController playerController) {
        WeaponController weapon = playerController.GetActiveWeapon();
        if (weapon != null) weapon.LeftBasicAttack();
    }
}
