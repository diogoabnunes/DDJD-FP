using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy2Controller))]
public class PunchGroundAttack : Attack
{
    public float range = 5f;
    public float duration = 3f;
    public float damage = 1f;
    public float damageArea = 4f;
    
    Enemy2Controller enemy2Controller;
    PlayerManager playerManager;

    void Start() {
        enemy2Controller = GetComponent<Enemy2Controller>();
        playerManager = PlayerManager.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttack() {
        Debug.Log("Punch Ground Attack!");

        enemy2Controller.AttackStarted();

        enemy2Controller.StopMovement();

        // time until he punches the ground
        yield return new WaitForSeconds(duration);

        // after the enemy hits the ground
        Debug.Log("Missing time of when enemy 2 hits ground");

        Vector3 impactPoint = enemy2Controller.GetEnemyPosition();
        bool playerHit = playerManager.PlayerWithinArea(impactPoint, damageArea);
        if (playerHit) {
            Debug.Log("Player was Hit!");
            playerManager.TakeDamage(damage);
        }

        enemy2Controller.AttackEnded();

        DefineNextAttackTime();

        yield return null;
    }
}
