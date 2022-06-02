using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class ChargeAttack : Attack
{
    public float maxRange = 50f;
    public float minRange = 25f;
    public float duration = 3f;
    public float damage = 1f;

    public float speed = 10f;
    public float stopChasingPlayerAtDistance = 10f;

    public float CHARGE_DURATION = 2f;
    public float REST_DURATION = 2f;

    BossController bossController;
    PlayerModel playerModel;

    Animator m_Animator;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();

        minRange = maxRange / 2f;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer >= minRange && distanceToPlayer <= maxRange && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Boss Bite Attack");

        bossController.Lock();

        bossController.CancelMovement();

        yield return new WaitForSeconds(CHARGE_DURATION);

        float initialSpeed = bossController.GetSpeed();
        bossController.SetSpeed(speed);

        do {
            Vector3 playerPosition = bossController.GetPlayerPosition();
            Vector3 currentPosition = bossController.GetEnemyPosition();
            float distance = Vector3.Distance(currentPosition, playerPosition);

            if (distance == 0) break;

            if (distance > stopChasingPlayerAtDistance) {
                bossController.ChasePlayer();
            }
            
        } while (true);

        bossController.SetSpeed(initialSpeed);

        // play animation of attack
        // m_Animator.SetTrigger("attack");

        // verify if player is still in range
        interactionManager.manageInteraction(new TakeDamage(damage, playerModel));

        yield return new WaitForSeconds(CHARGE_DURATION);

        bossController.Unlock();

        yield return null;
    }

    void Charge() {

    }

    void Sprint() {}
}
