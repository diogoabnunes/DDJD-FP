using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator m_Animator;

    public int damage = 10;
    
    public float coolDownBasicAttack = 0f;
    public float coolDownAbility1 = 0f;
    public float coolDownAbility2 = 0f;
    public float coolDownAbility3 = 0f;

    float nextTimeBasicAttack = -1f;
    float nextTimeAbility1 = -1f;
    float nextTimeAbility2 = -1f;
    float nextTimeAbility3 = -1f;

    protected void Start() {
        m_Animator.SetBool("hasGun", false);
    }

    public virtual void Enable() {}

    public virtual void Disable() {}

    public virtual void BasicAttack() {}

    public virtual void Ability1() {}

    public virtual void Ability2() {}

    public virtual void Ability3() {}

    protected bool CanDoBasicAttack() {
        return Time.time >= nextTimeBasicAttack;
    }

    protected bool CanDoAbility1() {
        return Time.time >= nextTimeAbility1;
    }

    protected bool CanDoAbility2() {
        return Time.time >= nextTimeAbility2;
    }

    protected bool CanDoAbility3() {
        return Time.time >= nextTimeAbility3;
    }

    protected void SetNextBasicAttackTime() {
        nextTimeBasicAttack = Time.time + coolDownBasicAttack;
    }

    protected void SetNextAbility1Time() {
        nextTimeAbility1 = Time.time + coolDownAbility1;
    }

    protected void SetNextAbility2Time() {
        nextTimeAbility2 = Time.time + coolDownAbility2;
    }

    protected void SetNextAbility3Time() {
        nextTimeAbility3 = Time.time + coolDownAbility3;
    }
}
