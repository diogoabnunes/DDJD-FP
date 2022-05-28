using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : WeaponController
{
    public GameObject Scythe;
    
    public float damage = 10;

    void Start() {
        base.Start();

        Scythe.SetActive(true);
    }

    public override void Enable() {
        Scythe.SetActive(true);
    }

    public override void Disable() {
        Scythe.SetActive(false);
    }

    public override void ExecuteBasicAttack() {
        m_Animator.SetTrigger("attack1");
    }

    public override void ExecuteAbility1() {
        Debug.Log("Scythe Ability 1");

        float targetAngle = playerController.GetTargetAngleTowardsCameraDirection(Vector3.forward);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playerController.MovePlayer(moveDir.normalized * 30f * (1.5f / 3));
    }

    public override void ExecuteAbility2() {
        Debug.Log("Scythe Ability 2");
    }

    private void OnTriggerEnter(Collider other) {
        if (!IsLocked()) return;

        EnemyController controller = other.gameObject.GetComponent<EnemyController>();
        if (controller != null) {
            controller.TakeDamage(damage);
        }
    }
}
