using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BigMuncherController))]
public class FireballAttack : Attack
{
    public GameObject fireball;
    public float minRange = 8f;
    public float maxRange = 10f;
    public float damageArea = 3f;

    BigMuncherController bigMuncherController;

    override public void Start() {
        base.Start();

        bigMuncherController = GetComponent<BigMuncherController>();
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer >= minRange && distanceToPlayer <= maxRange && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        Debug.Log("Fireball Attack!");

        bigMuncherController.Lock();

        bigMuncherController.CancelMovement();

        LaunchFireball();

        yield return new WaitForSeconds(3f);

        bigMuncherController.Unlock();

        DefineNextAttackTime();

        yield return null;
    }

    void LaunchFireball() {
        Vector3 initialPosition = bigMuncherController.GetEnemyPosition();
        initialPosition.y = initialPosition.y + 4f;

        Vector3 finalPosition = bigMuncherController.GetPlayerPosition();

        GameObject obj = Instantiate(fireball, initialPosition, Quaternion.identity);
        obj.GetComponent<FireballController>().SetTarget(finalPosition);
    }
}
