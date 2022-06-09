using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmallMuncherController))]
public class SmallMuncherAttack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;
    public SmallMuncherBlade blade;

    SmallMuncherController smallMuncherController;
    PlayerModel playerModel;

    Animator m_Animator;

    override public void Start() {
        base.Start();

        smallMuncherController = GetComponent<SmallMuncherController>();
        playerModel = PlayerModel.instance;
        m_Animator = smallMuncherController.GetAnimator();
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range;
    }

    public override IEnumerator DoAttackCoroutine() {
        smallMuncherController.Lock();

        smallMuncherController.CancelMovement();

        // play animation of attack
        m_Animator.SetTrigger("attack");
        
        blade.enabled = true;
        
        yield return new WaitForSeconds(duration);

        blade.enabled = false;
        
        smallMuncherController.Unlock();

        yield return null;
    }
}
