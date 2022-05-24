using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmallMuncherController))]
public class SmallMuncherAttack : Attack
{
    public float range = 2.5f;
    public float duration = 3f;
    public float damage = 1f;

    SmallMuncherController smallMuncherController;
    PlayerManager playerManager;

    void Start() {
        smallMuncherController = GetComponent<SmallMuncherController>();
        playerManager = PlayerManager.instance;
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range;
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Enemy 4 Attack");

        smallMuncherController.Lock();

        smallMuncherController.StopMovement();

        // play animation of attack
        // verify if player is still in range
        playerManager.TakeDamage(damage);

        yield return new WaitForSeconds(duration);

        smallMuncherController.Unlock();

        yield return null;
    }
}
