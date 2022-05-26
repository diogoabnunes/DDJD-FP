using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeaponCommand : Command
{
    public override void execute(PlayerController playerController) {
        Debug.Log("Swap Weapon Command");
    }
}
