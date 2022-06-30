using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GunsController : WeaponController
{
    public GameObject GunL;
    public GameObject GunR;

    public Transform bullet;
    public GameObject Bullet;
    public Transform leftBulletSpawnPoint;
    public Transform rightBulletSpawnPoint;
    public LayerMask spawnerLayerMask;

    public float recoil = 1f;

    string[] basicAttackAnimationNames = {"leftGunBasicAttack", "rightGunBasicAttack"};
    
    int currentGun = 0;

    override public void Start() {
        base.Start();

        GunL.SetActive(false);
        GunR.SetActive(false);
    }

    public override void Enable() {
        GunL.SetActive(true);
        GunR.SetActive(true);

        m_Animator.SetTrigger("switchToGuns");
    }

    public override void Disable() {
        m_Animator.SetTrigger("switchToScythe");

        GunL.SetActive(false);
        GunR.SetActive(false);
    }

    public override void ExecuteLeftBasicAttack() {
        Transform bulletSpawnPoint = leftBulletSpawnPoint;

        if (currentGun == 1) bulletSpawnPoint = rightBulletSpawnPoint;

        m_Animator.SetTrigger(basicAttackAnimationNames[currentGun]);

        Vector3 targetPoint = GetTargetPoint();

        Vector3 aim_dir = (targetPoint - bulletSpawnPoint.position);

        playerController.RotatePlayerWithVector(aim_dir);

        GameObject spawnedBullet = Instantiate(Bullet, bulletSpawnPoint.position, Quaternion.identity);

        spawnedBullet.GetComponent<BulletProjectile>().direction = aim_dir;

        currentGun = (currentGun + 1) % basicAttackAnimationNames.Length;
    }

    public override void ExecuteAbility1() {
        Debug.Log("Guns Ability 1");
    }

    public override void ExecuteAbility2() {
        playerController.BackFlip();
    }

    Vector3 GetTargetPoint() {
        Vector3 targetPoint = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity,  ~spawnerLayerMask.value)){
            targetPoint = raycastHit.point;
        }

        return targetPoint;
    }
}
