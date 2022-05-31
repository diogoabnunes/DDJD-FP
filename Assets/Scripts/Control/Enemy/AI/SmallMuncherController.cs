using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmallMuncherAttack))]
public class SmallMuncherController : EnemyController
{
    SmallMuncherAttack smallMuncherAttack;

    void Start() {
        base.Start();
        smallMuncherAttack = GetComponent<SmallMuncherAttack>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) return new AttackAction(smallMuncherAttack);
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);

        return null;
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return smallMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    public override void ManageAnimations() {
      if (isRunning()) {
        m_Animator.SetBool("isRunning", true);
      } else {
        m_Animator.SetBool("isRunning", false);
      }
    }

    public override void Die() {
      m_Animator.SetTrigger("die");
      dead = true;
      StartCoroutine(DieDelay());
    }
}
