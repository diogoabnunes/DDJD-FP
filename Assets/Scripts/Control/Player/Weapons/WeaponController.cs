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

    bool locked = false;

    protected void Start() {
        m_Animator.SetBool("hasGun", false);
    }

    public virtual void Enable() {}

    public virtual void Disable() {}

    public void BasicAttack() {
        if (!CanDoBasicAttack()) return;
        
        Lock();

        ExecuteBasicAttack();

        SetNextBasicAttackTime();

        StartCoroutine(UnlockWhenTimeElapsed(coolDownBasicAttack));
    }

    public void Ability1() {
        if (!CanDoAbility1()) return;

        Lock();

        ExecuteAbility1();

        SetNextAbility1Time();

        StartCoroutine(UnlockWhenTimeElapsed(coolDownAbility1));
    }

    public void Ability2() {
        if (!CanDoAbility2()) return;

        Lock();

        ExecuteAbility2();

        SetNextAbility2Time();

        StartCoroutine(UnlockWhenTimeElapsed(coolDownAbility2));
    }

    public void Ability3() {
        if (!CanDoAbility3()) return;

        Lock();

        ExecuteAbility3();

        SetNextAbility3Time();

        StartCoroutine(UnlockWhenTimeElapsed(coolDownAbility3));
    }

    public virtual void ExecuteBasicAttack() {}

    public virtual void ExecuteAbility1() {}

    public virtual void ExecuteAbility2() {}

    public virtual void ExecuteAbility3() {}

    protected bool CanDoBasicAttack() {
        return !IsLocked() && Time.time >= nextTimeBasicAttack;
    }

    protected bool CanDoAbility1() {
        return !IsLocked() && Time.time >= nextTimeAbility1;
    }

    protected bool CanDoAbility2() {
        return !IsLocked() && Time.time >= nextTimeAbility2;
    }

    protected bool CanDoAbility3() {
        return !IsLocked() && Time.time >= nextTimeAbility3;
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

    protected void Lock() {
        locked = true;
    }

    protected void Unlock() {
        locked = false;
    }

    public bool IsLocked() {
        return locked;
    }

    protected IEnumerator UnlockWhenTimeElapsed(float time) {
        yield return new WaitForSeconds(time);

        Unlock();
    }
}
