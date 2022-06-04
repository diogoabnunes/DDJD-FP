using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class ChargeAttack : Attack
{
    public float maxRange = 70f;
    public float minRange = 30f;
    public float damage = 1f;

    public float speed = 100f;

    public float CHARGE_DURATION = 2f;
    public float STUNNED_DURATION = 5f;
    public float REST_DURATION = 2f;

    bool dashing = false;

    GameObject chargeImpactPoint;
    GameObject frontPoint;

    BossController bossController;
    PlayerModel playerModel;

    Animator m_Animator;

    enum CollisionType {
        PlayerCollision,
        ObjectCollision,
        NoCollision,
        Null
    };

    CollisionType collision = CollisionType.Null;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();

        chargeImpactPoint = transform.Find("ChargeImpactPoint").gameObject;
        frontPoint = transform.Find("FrontPoint").gameObject;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer >= minRange && distanceToPlayer <= maxRange && TimeElapsedForAttack();
    }

    public void EnemyCollidedWithPlayer() {
        dashing = false;
        collision = CollisionType.PlayerCollision;
    }

    public void EnemyCollidedWithObject() {
        dashing = false;
        collision = CollisionType.ObjectCollision;
    }

    public void EnemyCollidedWithEndOfArena() {
        dashing = false;
        collision = CollisionType.NoCollision;
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Boss Bite Attack");

        bossController.Lock();

        bossController.CancelMovement();

        chargeImpactPoint.SetActive(true);

        Debug.Log("Charging");

        float startChargingTime = Time.time;
        while (Time.time - startChargingTime < CHARGE_DURATION) {
            Quaternion rotationTowardsPlayer = bossController.ComputeRotationTowardsPlayer();
            bossController.FacePlayer(rotationTowardsPlayer);
            
            yield return null;
        }

        Debug.Log("Dash");

        dashing = true;

        Vector3 velocity = GetVelocity();
        bossController.DisableAI();
        bossController.SetRigidbodyVelocity(velocity);

        while (dashing) {
            yield return null;
        }

        Debug.Log("Stopped");

        chargeImpactPoint.SetActive(false);
        
        bossController.RemoveRigidbodyVelocity();
        bossController.EnableAI();
        
        if (collision == CollisionType.PlayerCollision) {
            Debug.Log("Player Collision");
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));
            yield return new WaitForSeconds(REST_DURATION);
        }
        else if (collision == CollisionType.ObjectCollision) {
            Debug.Log("Stunned");
            yield return new WaitForSeconds(STUNNED_DURATION);
        }
        else {
            Debug.Log("Arena");
            yield return new WaitForSeconds(REST_DURATION);
        }

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }

    Vector3 GetVelocity() {
        Vector3 destination = frontPoint.transform.position;
        Vector3 currentPosition = bossController.GetEnemyPosition();
        Vector3 direction = (destination - currentPosition).normalized;

        Vector3 velocity = direction * speed;
        velocity.y = 0;

        return velocity;
    }
}
