using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : WeaponController
{
    public GameObject GunL;
    public GameObject GunR;

    public Transform bullet;
    public Transform bulletSpawnPoint;

    public float fireRate = 30f;
    public float impactForce = 3f;

    public float lerpRatio = 20f;

    void Start() {
        base.Start();

        GunL.SetActive(false);
        GunR.SetActive(false);
    }

    public override void Enable() {
        GunL.SetActive(true);
        GunR.SetActive(true);

        m_Animator.SetBool("hasGun", true);
    }

    public override void Disable() {
        m_Animator.SetBool("hasGun", false);

        GunL.SetActive(false);
        GunR.SetActive(false);
    }

    public override void BasicAttack() {
        if (!CanDoBasicAttack()) return;

        Debug.Log("Gun Attack");

        /*Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f)){
            mouseWorldPosition = raycastHit.point;
        }

        Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;

        Vector3 worldAimTarget = mouseWorldPosition;
        
        worldAimTarget.y = transform.position.y;

        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * lerpRatio);

        
        transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));*/

        SetNextBasicAttackTime();
    }

    public override void Ability1() {
        if (!CanDoAbility1()) return;

        Debug.Log("Guns Ability 1");

        SetNextAbility1Time();
    }

    public override void Ability2() {
        if (!CanDoAbility2()) return;

        Debug.Log("Guns Ability 2");

        SetNextAbility2Time();
    }

    public override void Ability3() {
        if (!CanDoAbility3()) return;

        Debug.Log("Guns Ability 3");

        SetNextAbility3Time();
    }
}
