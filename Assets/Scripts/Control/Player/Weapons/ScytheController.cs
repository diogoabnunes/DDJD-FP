using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : WeaponController
{
    public GameObject Scythe;

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
    }

    public override void ExecuteAbility2() {
        Debug.Log("Scythe Ability 2");
    }

    public override void ExecuteAbility3() {
        Debug.Log("Scythe Ability 3");
    }

    private void OnTriggerEnter(Collider other) {
        if (!IsLocked()) return;

        EnemyController controller = other.gameObject.GetComponent<EnemyController>();
        if (controller != null) {
            controller.TakeDamage(damage);
        }
    }
}