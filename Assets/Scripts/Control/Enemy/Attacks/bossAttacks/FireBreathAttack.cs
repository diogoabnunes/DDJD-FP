using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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

    public GameObject leftFire;

    public GameObject rightFire;

    public GameObject middleFire;

    public Transform arenaCenterPoint;

    [EventRef, SerializeField] string fireBreath = default;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();
    }

    public override bool CanAttack(float distanceToPlayer) {
        return TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        

        bossController.Lock();

        

        float stoppingDistance = bossController.GetStoppingDistance();
        bossController.SetStoppingDistance(0f);
        bossController.GoTo(arenaCenterPoint.position);

        Vector3 enemyPosition = bossController.GetEnemyPosition();
        while (enemyPosition.x != arenaCenterPoint.position.x || enemyPosition.z != arenaCenterPoint.position.z) {
            yield return null;
            enemyPosition = bossController.GetEnemyPosition();
        }

        bossController.SetStoppingDistance(stoppingDistance);

        

        // look for player
        float startLookingTime = Time.time;
        while (Time.time - startLookingTime < LOOK_DURATION) {
            m_Animator.SetBool("breathingFire_Prep", true);
            Quaternion rotationTowardsPlayer = bossController.ComputeRotationTowardsPlayer();
            bossController.FacePlayer(rotationTowardsPlayer);

            yield return null;
        }
        m_Animator.SetBool("breathingFire_Prep", false);
        m_Animator.SetBool("breathingFire", true);

        rightFire.GetComponent<ParticleSystem>().Play();
        leftFire.GetComponent<ParticleSystem>().Play();
        middleFire.GetComponent<ParticleSystem>().Play();


        

        var audioEvent = RuntimeManager.CreateInstance(fireBreath);
        audioEvent.start();
        audioEvent.release();

        // breath fire
        float startBreathingTime = Time.time;
        while (Time.time - startBreathingTime < FIRE_BREATH_DURATION) {
            // verify if player is still in range
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));

            yield return null;
        }

        rightFire.GetComponent<ParticleSystem>().Stop();
        leftFire.GetComponent<ParticleSystem>().Stop();
        middleFire.GetComponent<ParticleSystem>().Stop();

        m_Animator.SetBool("breathingFire", false);



        

        yield return new WaitForSeconds(REST_DURATION);

        

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }
}
