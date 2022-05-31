using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmallMuncherAttack))]
public class SmallMuncherController : EnemyController
{
    SmallMuncherAttack smallMuncherAttack;

    SkinnedMeshRenderer renderer;
    public GameObject rendererHolder;
    public Material dissolveMaterial;
    float dissolvedPercentage = 0f;

    void Start() {
        base.Start();
        smallMuncherAttack = GetComponent<SmallMuncherAttack>();
        renderer = rendererHolder.GetComponentInChildren<SkinnedMeshRenderer>();
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

    public override void DeadAnimation() {
      dissolvedPercentage = dissolvedPercentage + 0.01f;
      renderer.materials[0].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
      renderer.materials[1].SetFloat("Vector1_89f3df7da7884450b303f423e3242b03", dissolvedPercentage);
    }

    public override void Die() {
      m_Animator.SetTrigger("die");
      dead = true;
      StartCoroutine(Dissolve());
    }

    public IEnumerator Dissolve() {
      yield return new WaitForSeconds(1);
      InvokeRepeating("DeadAnimation", 0f, 0.01f);

      StartCoroutine(DieDelay());
    }
}
