using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class FireBreathAttack : Attack
{
    public float duration = 3f;
    public float damage = 1f;
    public float LOOK_DURATION = 1.5f;
    public float FIRE_BREATH_DURATION = 2f;
    public float REST_DURATION = 1f;

    BossController bossController;
    PlayerModel playerModel;

    Animator m_Animator;

    Vector3 arenaCenterPoint;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();

        arenaCenterPoint = Vector3.zero;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Fire Breath Attack");

        bossController.Lock();

        bossController.CancelMovement();

        Debug.Log("Moving to the center of the arena!");

        // move to center of arena
        bossController.GoTo(arenaCenterPoint);

        Vector3 enemyPosition = bossController.GetEnemyPosition();
        enemyPosition.y = 0;
        while (enemyPosition != arenaCenterPoint) {
            yield return null;
            enemyPosition = bossController.GetEnemyPosition();
            enemyPosition.y = 0;
        }

        Debug.Log("Facing player!");

        // look for player
        float startLookingTime = Time.time;
        while (Time.time - startLookingTime < LOOK_DURATION) {
            Quaternion rotationTowardsPlayer = bossController.ComputeRotationTowardsPlayer();
            bossController.FacePlayer(rotationTowardsPlayer);

            yield return null;
        }

        Debug.Log("Breathing fire!");

        // breath fire
        float startBreathingTime = Time.time;
        while (Time.time - startBreathingTime < FIRE_BREATH_DURATION) {
            // verify if player is still in range
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));

            yield return null;
        }

        Debug.Log("Resting!");

        yield return new WaitForSeconds(REST_DURATION);

        Debug.Log("Done!");

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }
}
