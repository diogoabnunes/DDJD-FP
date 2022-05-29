using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject player;
    protected PlayerController playerController;

    public Animator m_Animator;
    
    public float coolDownBasicAttack = 0f;
    public float coolDownAbility1 = 0f;
    public float coolDownAbility2 = 0f;

    float nextTimeBasicAttack = -1f;
    float nextTimeAbility1 = -1f;
    float nextTimeAbility2 = -1f;

    bool locked = false;

    protected void Start() {
        playerController = player.GetComponent<PlayerController>();

        m_Animator.SetBool("hasGun", false);
    }

    public virtual void Enable() {}

    public virtual void Disable() {}

    public void BasicAttack() {
        if (!CanDoBasicAttack()) return;
        
        Lock();

        ExecuteBasicAttack();

        SetNextBasicAttackTime();

        StartCoroutine(UnlockWhenTimeElapsed());
    }

    public void Ability1() {
        if (!CanDoAbility1()) return;

        Lock();

        ExecuteAbility1();

        SetNextAbility1Time();

        StartCoroutine(UnlockWhenTimeElapsed());
    }

    public void Ability2() {
        if (!CanDoAbility2()) return;

        Lock();

        ExecuteAbility2();

        SetNextAbility2Time();

        StartCoroutine(UnlockWhenTimeElapsed());
    }

    public virtual void ExecuteBasicAttack() {}

    public virtual void ExecuteAbility1() {}

    public virtual void ExecuteAbility2() {}

    protected bool CanDoBasicAttack() {
        return !IsLocked() && Time.time >= nextTimeBasicAttack;
    }

    protected bool CanDoAbility1() {
        return !IsLocked() && Time.time >= nextTimeAbility1;
    }

    protected bool CanDoAbility2() {
        return !IsLocked() && Time.time >= nextTimeAbility2;
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

    protected void Lock() {
        locked = true;
    }

    protected void Unlock() {
        locked = false;
    }

    public bool IsLocked() {
        return locked;
    }

    protected IEnumerator UnlockWhenTimeElapsed() {
        // AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(1);
        // while(!state.IsTag("WaitFor")){
        //     yield return null;
        //     state = m_Animator.GetCurrentAnimatorStateInfo(1);
        // }

        // yield return new WaitForSeconds(state.length);
        yield return new WaitForSeconds(0.5f);

        Unlock();
    }
}
