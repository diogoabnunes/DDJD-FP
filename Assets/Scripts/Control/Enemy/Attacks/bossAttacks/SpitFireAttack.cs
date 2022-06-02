using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class SpitFireAttack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;
    public float FIRE_DURATION = 2f;

    BossController bossController;
    PlayerModel playerModel;

    Transform arenaCenterPoint;

    Animator m_Animator;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Boss Bite Attack");

        bossController.Lock();

        bossController.CancelMovement();

        // play animation of attack
        // m_Animator.SetTrigger("attack");

        // go to the center of the arena

        float startTime = Time.time;
        while (Time.time - startTime < FIRE_DURATION) {
            // verify if player is still in range
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));

            yield return null;
        }

        // yield return new WaitForSeconds(duration);

        bossController.Unlock();

        yield return null;
    }
}
