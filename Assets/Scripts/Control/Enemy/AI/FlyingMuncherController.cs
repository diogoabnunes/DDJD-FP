using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingMuncherAttack))]
public class FlyingMuncherController : EnemyController
{
    FlyingMuncherAttack flyingMuncherAttack;

    SkinnedMeshRenderer renderer;
    public GameObject rendererHolder;
    float dissolvedPercentage = 0f;

    void Start() {
        base.Start();
        flyingMuncherAttack = GetComponent<FlyingMuncherAttack>();
        renderer = rendererHolder.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public override Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) return new AttackAction(flyingMuncherAttack);
        if (PlayerInLookRange(distanceToPlayer)) return new ChaseAction(base.gameObject, rotationTowardsPlayer);

        return null;
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return flyingMuncherAttack.CanAttack(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    public override void ManageAnimations() {
      if (isRunning()) {
        m_Animator.SetBool("isWalking", true);
      } else {
        m_Animator.SetBool("isWalking", false);
      }
    }

    public override void DeadAnimation() {
      dissolvedPercentage = dissolvedPercentage + 0.01f;
      renderer.materials[0].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
      renderer.materials[1].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
    }

    public override void Die() {
      m_Animator.SetTrigger("die");
      dead = true;
      StopMovement();
      StartCoroutine(Dissolve());
    }

    public IEnumerator Dissolve() {
      yield return new WaitForSeconds(1);
      InvokeRepeating("DeadAnimation", 0f, 0.01f);

      StartCoroutine(DieDelay());
    }
}
