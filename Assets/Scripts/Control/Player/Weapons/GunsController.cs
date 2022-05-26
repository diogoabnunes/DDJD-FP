using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : WeaponController
{
    public override void BasicAttack() {
        Debug.Log("Guns Basic Attack");
    }

    public override void Ability1() {
        Debug.Log("Guns Ability 1");
    }

    public override void Ability2() {
        Debug.Log("Guns Ability 2");
    }

    public override void Ability3() {
        Debug.Log("Guns Ability 3");
    }
}
