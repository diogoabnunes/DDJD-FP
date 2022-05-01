using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy2Controller))]
public class FireballAttack : Attack
{
    public GameObject fireball;
    public float minRange = 8f;
    public float maxRange = 10f;

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

        Vector3 position = enemy2Controller.GetEnemyPosition();
        position.y = position.y + 3.5f;
        GameObject obj = Instantiate(fireball, position, Quaternion.identity);
        obj.GetComponent<FireballController>().finalPosition = enemy2Controller.GetPlayerPosition();

        yield return new WaitForSeconds(3f);
        
        enemy2Controller.AttackEnded();

        DefineNextAttackTime();

        yield return null;
    }
}
