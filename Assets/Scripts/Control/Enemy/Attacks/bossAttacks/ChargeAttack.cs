using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class ChargeAttack : Attack
{
    public float maxRange = 70f;
    public float minRange = 30f;
    public float damage = 1f;

    public float speed = 50f;

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
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer >= minRange && distanceToPlayer <= maxRange && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Boss Bite Attack");

        bossController.Lock();

        bossController.CancelMovement();

        Debug.Log("Charging");

        float startChargingTime = Time.time;
        while (Time.time - startChargingTime < CHARGE_DURATION) {
            Quaternion rotationTowardsPlayer = bossController.ComputeRotationTowardsPlayer();
            bossController.FacePlayer(rotationTowardsPlayer);
            
            yield return null;
        }

        Debug.Log("Dash");

        Vector3 destination = bossController.GetPlayerPosition();

        float initialSpeed = bossController.GetSpeed();
        bossController.SetSpeed(speed);

        bossController.GoTo(destination);

        Vector3 enemyPosition = bossController.GetEnemyPosition();
        while (enemyPosition.x != destination.x || enemyPosition.z != destination.z) {
            yield return null;
            enemyPosition = bossController.GetEnemyPosition();
        }

        bossController.SetSpeed(initialSpeed);

        Debug.Log("Resting");

        yield return new WaitForSeconds(REST_DURATION);

        // verify if player is still in range
        // interactionManager.manageInteraction(new TakeDamage(damage, playerModel));

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }
}
