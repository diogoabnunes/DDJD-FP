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

    Animator m_Animator;

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
        m_Animator.SetTrigger("groundAttack");

        // time until he punches the ground
        yield return new WaitForSeconds(duration);

        // after the enemy hits the ground
        

        PunchGround();

        bigMuncherController.Unlock();

        DefineNextAttackTime();

        yield return null;
    }

    void PunchGround() {
        Vector3 impactPoint = bigMuncherController.GetEnemyPosition();

        bool playerHit = playerModel.PlayerWithinArea(impactPoint, damageArea);
        if (playerHit) {
            
            interactionManager.manageInteraction(new TakeDamage(damage, playerModel));
        }
    }
}
