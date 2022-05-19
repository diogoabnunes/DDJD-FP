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
    PlayerManager playerManager;

    void Start() {
        bigMuncherController = GetComponent<BigMuncherController>();
        playerManager = PlayerManager.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttack() {
        Debug.Log("Punch Ground Attack!");

        bigMuncherController.AttackStarted();

        bigMuncherController.StopMovement();

        // time until he punches the ground
        yield return new WaitForSeconds(duration);

        // after the enemy hits the ground
        Debug.Log("Missing time of when enemy 2 hits ground");

        PunchGround();

        bigMuncherController.AttackEnded();

        DefineNextAttackTime();

        yield return null;
    }

    void PunchGround() {
        Vector3 impactPoint = bigMuncherController.GetEnemyPosition();

        bigMuncherController.DrawDamageArea(impactPoint, damageArea);

        bool playerHit = playerManager.PlayerWithinArea(impactPoint, damageArea);
        if (playerHit) {
            Debug.Log("Player was Hit!");
            playerManager.TakeDamage(damage);
        }
    }
}
