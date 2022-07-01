using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    float normalSpeed;
    Vector3 dashDestination;

    override public void Start() {
        base.Start();

        bossController = GetComponent<BossController>();
        playerModel = PlayerModel.instance;
        m_Animator = bossController.GetAnimator();

        chargeImpactPoint = transform.Find("ChargeImpactPoint").gameObject;
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
        // 

        bossController.Lock();

        bossController.CancelMovement();

        // 

        float startChargingTime = Time.time;
        while (Time.time - startChargingTime < CHARGE_DURATION) {
            Quaternion rotationTowardsPlayer = bossController.ComputeRotationTowardsPlayer();
            bossController.FacePlayer(rotationTowardsPlayer);
            
            yield return null;
        }

        Dash();

        while (IsDashing()) {
            
            yield return null;
        }
        
        StopDash();
        
        float time = ResolveCollisionAndGetTimeStoppedAfterCollision();
        yield return new WaitForSeconds(time);

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }

    float ResolveCollisionAndGetTimeStoppedAfterCollision() {
        ResolveCollsiion();
        return GetTimeStoppedAfterCollision();
    }

    void ResolveCollsiion() {
        if (CollidedWithPlayer()) {
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));
        }
    }

    float GetTimeStoppedAfterCollision() {
        if (CollidedWithObject()) {
            // 
            return STUNNED_DURATION;
        }
        else {
            // 
            return REST_DURATION;
        }
    }

    bool CollidedWithPlayer() {
        return collision == CollisionType.PlayerCollision;
    }

    bool CollidedWithObject() {
        return collision == CollisionType.ObjectCollision;
    }

    bool IsDashing() {
        return !CollidedWithSomething() && !ReachedDestination();
    }

    void Dash() {
        // 

        SetupDash();

        dashDestination = bossController.GetPlayerPosition();
        bossController.GoTo(dashDestination);
        m_Animator.SetBool("dashing", true);
    }

    void SetupDash() {
        dashing = true;
        chargeImpactPoint.SetActive(true);

        ChangeToDashAgentType();
        ChangeToDashSpeed();
    }

    void ChangeToDashSpeed() {
        normalSpeed = bossController.GetAgentSpeed();
        bossController.SetAgentSpeed(speed);
    }

    void ChangeToDashAgentType() {
        bossController.AlternateAIAgent(GetAgentIdByName("BossDash"));
    }

    void StopDash() {
        // 

        StopMovement();
        
        ResetDash();
    }

    void StopMovement() {
        bossController.CancelMovement();
    }

    void ResetDash() {
        dashing = false;
        chargeImpactPoint.SetActive(false);

        m_Animator.SetBool("dashing", false);
        ResetAgentType();
        ResetSpeed();
    }

    void ResetAgentType() {
        bossController.AlternateAIAgent(GetAgentIdByName("Boss"));
    }

    void ResetSpeed() {
        bossController.SetAgentSpeed(normalSpeed);
    }

    bool CollidedWithSomething() {
        return !dashing;
    }

    bool ReachedDestination() {
        Vector3 currentPosition = bossController.GetEnemyPosition();
        return currentPosition.x == dashDestination.x && currentPosition.z == dashDestination.z;
    }

    // -----------------------------------------------------------------

    static int GetAgentIdByName(string agentTypeName)
    {
         int count = NavMesh.GetSettingsCount();
         string[] agentTypeNames = new string[count + 2];
         for (var i = 0; i < count; i++)
         {
             int id = NavMesh.GetSettingsByIndex(i).agentTypeID;
             string name = NavMesh.GetSettingsNameFromID(id);
             if(name == agentTypeName)
             {
                 return id;
             }
         }
         return -1;
    }
}
