using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingMuncherController))]
public class FlyingMuncherAttack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;

    FlyingMuncherController flyingMuncherController;
    PlayerManager playerManager;

    void Start() {
        flyingMuncherController = GetComponent<FlyingMuncherController>();
        playerManager = PlayerManager.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range;
    }

    public override IEnumerator DoAttack() {
        Debug.Log("Enemy 3 Attack");

        flyingMuncherController.AttackStarted();

        flyingMuncherController.StopMovement();

        // play animation of attack
        // verify if player is still in range
        playerManager.TakeDamage(damage);

        yield return new WaitForSeconds(duration);

        flyingMuncherController.AttackEnded();

        yield return null;
    }
}
