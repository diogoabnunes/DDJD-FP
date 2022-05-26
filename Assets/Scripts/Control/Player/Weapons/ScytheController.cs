using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : WeaponController
{
    public GameObject Scythe;

    void Start() {
        base.Start();

        Scythe.SetActive(true);
    }

    public override void Enable() {
        Scythe.SetActive(true);
    }

    public override void Disable() {
        Scythe.SetActive(false);
    }

    public override void BasicAttack() {
        if (!CanDoBasicAttack()) return;

        Debug.Log("Scythe Basic Attack");

        m_Animator.SetTrigger("attack1");

        SetNextBasicAttackTime();
    }

    public override void Ability1() {
        if (!CanDoAbility1()) return;

        Debug.Log("Scythe Ability 1");

        SetNextAbility1Time();
    }

    public override void Ability2() {
        if (!CanDoAbility2()) return;

        Debug.Log("Scythe Ability 2");

        SetNextAbility2Time();
    }

    public override void Ability3() {
        if (!CanDoAbility3()) return;

        Debug.Log("Scythe Ability 3");

        SetNextAbility3Time();
    }
}
