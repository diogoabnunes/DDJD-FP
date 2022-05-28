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
    }

    public override void ExecuteAbility2() {
        Debug.Log("Scythe Ability 2");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("scythe attack before");
        if (!IsLocked()) return;

        Debug.Log("scythe attack");
        EnemyController controller = other.gameObject.GetComponent<EnemyController>();
        if (controller != null) {
            controller.TakeDamage(damage);
        }
    }
}
