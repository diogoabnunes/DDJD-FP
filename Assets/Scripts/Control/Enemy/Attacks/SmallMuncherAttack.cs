using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmallMuncherController))]
public class SmallMuncherAttack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;

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
        Debug.Log("Enemy 4 Attack");

        smallMuncherController.Lock();

        smallMuncherController.CancelMovement();

        // play animation of attack
        m_Animator.SetTrigger("attack");

        // verify if player is still in range
        interactionManager.manageInteraction(new TakeDamage(damage, playerModel));

        yield return new WaitForSeconds(duration);

        smallMuncherController.Unlock();

        yield return null;
    }
}
