using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BigMuncherController))]
public class PunchGroundAttack : Attack
{
    public float range = 5f;
    public float duration = 3f;
    public float damage = 1f;
    public float damageArea = 4f;

    BigMuncherController bigMuncherController;
    PlayerModel playerModel;

    void Start() {
        bigMuncherController = GetComponent<BigMuncherController>();
        playerModel = PlayerModel.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Punch Ground Attack!");

        bigMuncherController.Lock();

        bigMuncherController.StopMovement();

        // time until he punches the ground
        yield return new WaitForSeconds(duration);

        // after the enemy hits the ground
        Debug.Log("Missing time of when enemy 2 hits ground");

        PunchGround();

        bigMuncherController.Unlock();

        DefineNextAttackTime();

        yield return null;
    }

    void PunchGround() {
        Vector3 impactPoint = bigMuncherController.GetEnemyPosition();

        bool playerHit = playerModel.PlayerWithinArea(impactPoint, damageArea);
        if (playerHit) {
            Debug.Log("Player was Hit!");
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));
        }
    }
}
