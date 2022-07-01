using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingMuncherController))]
public class FlyingMuncherAttack : Attack
{
    public float range = 5f;
    public float duration = 3f;
    public float damage = 1f;
    public float TIME_ELAPSED_FROM_ANIMATION_START_UNTIL_SHOOT = 1F;
    public float destroyTime = 3.0f;    

    FlyingMuncherController flyingMuncherController;
    PlayerModel playerModel;

    Animator m_Animator;

    public GameObject bullet;
    public Transform firePoint;

    override public void Start() {
        base.Start();

        flyingMuncherController = GetComponent<FlyingMuncherController>();
        playerModel = PlayerModel.instance;
        m_Animator = flyingMuncherController.GetAnimator();
    }

    public override bool CanAttack(float distanceToPlayer/*, Quaternion rotationTowardsPlayer*/) {
        return distanceToPlayer <= range && TimeElapsedForAttack() /*&& IsFacingPlayer(rotationTowardsPlayer)*/;
    }

    public override IEnumerator DoAttackCoroutine() {
        

        flyingMuncherController.Lock();

        flyingMuncherController.CancelMovement();

        // play animation of attack
        m_Animator.SetTrigger("attack");

        // verify if player is still in range
        StartCoroutine(LaunchBullet());

        yield return new WaitForSeconds(duration);

        flyingMuncherController.Unlock();

        yield return null;
    }

    IEnumerator LaunchBullet() {
        Vector3 initialPosition = firePoint.position;

        Vector3 finalPosition = flyingMuncherController.GetPlayerPosition();

        yield return new WaitForSeconds(TIME_ELAPSED_FROM_ANIMATION_START_UNTIL_SHOOT);

        GameObject obj = Instantiate(bullet, initialPosition, Quaternion.identity);
        obj.GetComponent<EnemyBulletController>().SetTarget(finalPosition);
        obj.GetComponent<EnemyBulletController>().SetLauncher(this.gameObject);
        obj.GetComponent<EnemyBulletController>().setDamage(damage);

        Destroy(obj, destroyTime);
    }
}
