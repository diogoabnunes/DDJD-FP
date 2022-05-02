using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy2Controller))]
public class FireballAttack : Attack
{
    public GameObject fireball;
    public float minRange = 8f;
    public float maxRange = 10f;
    public float damageArea = 3f;

    Enemy2Controller enemy2Controller;

    void Start() {
        enemy2Controller = GetComponent<Enemy2Controller>();
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer >= minRange && distanceToPlayer <= maxRange && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttack() {
        Debug.Log("Fireball Attack!");

        enemy2Controller.AttackStarted();

        enemy2Controller.StopMovement();
        LaunchFireball();

        yield return new WaitForSeconds(3f);
        
        enemy2Controller.AttackEnded();

        DefineNextAttackTime();

        yield return null;
    }

    void LaunchFireball() {
        Vector3 initialPosition = enemy2Controller.GetEnemyPosition();
        initialPosition.y = initialPosition.y + 4f;

        Vector3 finalPosition = enemy2Controller.GetPlayerPosition();

        enemy2Controller.DrawDamageArea(finalPosition, damageArea);

        GameObject obj = Instantiate(fireball, initialPosition, Quaternion.identity);
        obj.GetComponent<FireballController>().SetTarget(finalPosition);
    }
}
