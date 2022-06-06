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

    float normalSpeed;
    Vector3 dashDestination;

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
        Debug.Log("Boss Charge Attack");

        bossController.Lock();

        bossController.CancelMovement();

        Debug.Log("Charging");

        float startChargingTime = Time.time;
        while (Time.time - startChargingTime < CHARGE_DURATION) {
            Quaternion rotationTowardsPlayer = bossController.ComputeRotationTowardsPlayer();
            bossController.FacePlayer(rotationTowardsPlayer);
            
            yield return null;
        }

        Dash();

        while (!CollidedWithSomething() && !ReachedDestination()) {
            yield return null;
        }

        StopDash();
        
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
            Debug.Log("Arena or Not find player");
            yield return new WaitForSeconds(REST_DURATION);
        }

        DefineNextAttackTime();

        bossController.Unlock();

        yield return null;
    }

    void Dash() {
        Debug.Log("Dash");

        SetupDash();

        dashDestination = bossController.GetPlayerPosition();
        bossController.GoTo(dashDestination);
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
        Debug.Log("Stopped");

        StopMovement();
        
        dashing = false;
        chargeImpactPoint.SetActive(false);

        ResetAgentType();
        ResetSpeed();
    }

    void StopMovement() {
        bossController.CancelMovement();
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
