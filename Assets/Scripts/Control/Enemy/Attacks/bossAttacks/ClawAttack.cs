using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class ClawAttack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;

    GameObject clawImpactPoint;

    BossController bossController;
    PlayerModel playerModel;

    Animator m_Animator;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();

        clawImpactPoint = transform.Find("ClawImpactPoint").gameObject;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Claw Attack");

        bossController.Lock();

        bossController.CancelMovement();

        clawImpactPoint.SetActive(true);

        // play animation of attack
        // m_Animator.SetTrigger("attack");

        yield return new WaitForSeconds(duration);

        clawImpactPoint.SetActive(false);

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }
}
