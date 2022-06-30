using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FMODUnity;

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

    [EventRef, SerializeField] string bulletSound = default;
    
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

        m_Animator.SetTrigger("leftGunBasicAttack");

        Shoot(bulletSpawnPoint);
    }

    public override void ExecuteRightBasicAttack() {
        Transform bulletSpawnPoint = rightBulletSpawnPoint;

        m_Animator.SetTrigger("rightGunBasicAttack");

        Shoot(bulletSpawnPoint);
    }

    private void Shoot(Transform bulletSpawnPoint){

        var audioEvent = RuntimeManager.CreateInstance(bulletSound);
        audioEvent.start();
        audioEvent.release();

        Vector3 targetPoint = GetTargetPoint();

        Vector3 aim_dir = (targetPoint - bulletSpawnPoint.position);

        playerController.RotatePlayerWithVector(aim_dir);

        GameObject spawnedBullet = Instantiate(Bullet, bulletSpawnPoint.position, Quaternion.identity);

        spawnedBullet.GetComponent<BulletProjectile>().direction = aim_dir;

        
        Destroy(spawnedBullet, 5f);
    }

    public override void ExecuteAbility1() {
        playerController.BackFlip();
    }

    public override void ExecuteAbility2() {
        StartCoroutine(Ability2Shooting());
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

    IEnumerator Ability2Shooting() {
        Lock();

        for(int i = 0; i < 15; i++){
            ExecuteLeftBasicAttack();
            yield return new WaitForSeconds(0.07f);
            ExecuteRightBasicAttack();
            yield return new WaitForSeconds(0.07f);
        }
        
        Unlock();
    }
}
