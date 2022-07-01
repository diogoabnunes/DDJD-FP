using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BigMuncherController))]
public class BigMuncherFireball : Attack
{
    public float range = 5f;
    public float duration = 3f;

    public float damage = 20f;
    public float TIME_ELAPSED_FROM_ANIMATION_START_UNTIL_SHOOT = 1F;
    public float destroyTime = 3.0f;    

    BigMuncherController bigMuncherController;
    PlayerModel playerModel;

    Animator m_Animator;

    public GameObject bullet;
    public Transform firePoint;

    override public void Start() {
        base.Start();

        bigMuncherController = GetComponent<BigMuncherController>();
        playerModel = PlayerModel.instance;
        m_Animator = bigMuncherController.GetAnimator();
    }

    public override bool CanAttack(float distanceToPlayer) {
        return distanceToPlayer <= range && TimeElapsedForAttack();
    }

    public override IEnumerator DoAttackCoroutine() {
        

        bigMuncherController.Lock();

        bigMuncherController.CancelMovement();

        // play animation of attack
        m_Animator.SetTrigger("fireball");

        // verify if player is still in range
        StartCoroutine(LaunchBullet());

        yield return new WaitForSeconds(duration);

        bigMuncherController.Unlock();

        yield return null;
    }

    IEnumerator LaunchBullet() {
        Vector3 initialPosition = firePoint.position;

        Vector3 offset = new Vector3(0.0f, 3.0f, 0.0f);
        
        Vector3 finalPosition = bigMuncherController.GetPlayerPosition() + offset;

        yield return new WaitForSeconds(TIME_ELAPSED_FROM_ANIMATION_START_UNTIL_SHOOT);

        GameObject obj = Instantiate(bullet, initialPosition, Quaternion.identity);
        obj.GetComponent<EnemyBulletController>().SetTarget(finalPosition);
        obj.GetComponent<EnemyBulletController>().SetLauncher(this.gameObject);
        obj.GetComponent<EnemyBulletController>().setDamage(damage);

        Destroy(obj, destroyTime);
    }
}
