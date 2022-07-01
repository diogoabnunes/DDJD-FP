using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject player;
    protected PlayerController playerController;
    protected PlayerModel playerModel;
    protected InteractionManager interactionManager;

    public Animator m_Animator;


    public float coolDownBasicAttack = 0.5f;
    public float coolDownAbility1 = 0.0f;
    public float coolDownAbility2 = 0.0f;
    public bool canAbility1 = true;
    public bool canAbility2 = true;

    float nextTimeBasicAttack = -1.0f;
    float nextTimeAbility1 = -1.0f;
    float nextTimeAbility2 = -1.0f;

    bool locked = false;

    public virtual void Start() {
        interactionManager = InteractionManager.instance;
        playerModel = PlayerModel.instance;

        playerController = player.GetComponent<PlayerController>();
        
        m_Animator.SetBool("hasGun", false);
    }

    public virtual void Enable() {}

    public virtual void Disable() {}
    public void resetAbilities(){
        canAbility1 = true;
        canAbility2 = true;
    }
    public void RightBasicAttack() {
        if (!CanDoBasicAttack()) return;

        Lock();

        ExecuteRightBasicAttack();

        StartCoroutine(UnlockWhenTimeElapsed());
    }

    public void LeftBasicAttack() {
        if (!CanDoBasicAttack()) return;

        Lock();

        ExecuteLeftBasicAttack();

        StartCoroutine(UnlockWhenTimeElapsed());
    }

    public void Ability1() {
        if (!CanDoAbility1()) return;

        ExecuteAbility1();
    }

    public void Ability2() {
        if (!CanDoAbility2()) return;

        ExecuteAbility2();
    }

    public virtual void ExecuteLeftBasicAttack() {}
    public virtual void ExecuteRightBasicAttack() {}

    public virtual void ExecuteAbility1() {}

    public virtual void ExecuteAbility2() {}

    protected bool CanDoBasicAttack() {
        
        return !IsLocked() && Time.time >= nextTimeBasicAttack;
    }

    protected bool CanDoAbility1() {
        
        return !IsLocked() && canAbility1;
    }

    protected bool CanDoAbility2() {
        
        return !IsLocked() && canAbility2;
    }

    protected IEnumerator CooldownAbility1 (){
        canAbility1 = false;
        
        yield return new WaitForSeconds(coolDownAbility1);

        canAbility1 = true;
    }

    protected IEnumerator CooldownAbility2 (){
        canAbility2 = false;
        
        yield return new WaitForSeconds(coolDownAbility2);

        canAbility2 = true;
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
        AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(1);
        while(!state.IsTag("WaitFor")){
            yield return null;
            state = m_Animator.GetCurrentAnimatorStateInfo(1);
        }

        yield return new WaitForSeconds(state.length);
        // yield return new WaitForSeconds(0.5f);

        Unlock();
    }
}
