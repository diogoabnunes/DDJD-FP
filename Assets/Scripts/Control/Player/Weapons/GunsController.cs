using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : WeaponController
{
    public GameObject GunL;
    public GameObject GunR;

    public Transform bullet;
    public Transform rightBulletSpawnPoint;
    public Transform leftBulletSpawnPoint;

    public float recoil = 1f;

    string[] basicAttackAnimationNames = {"leftGunBasicAttack", "rightGunBasicAttack"};

    bool shootLeft = true;

    // public float fireRate = 30f;
    // public float impactForce = 3f;

    void Start() {
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

    public override void ExecuteBasicAttack() {
        float targetAngle = playerController.GetTargetAngleTowardsCameraDirection(Vector3.zero);
        playerController.RotatePlayer(targetAngle, 0f);

        Vector3 targetPoint = GetTargetPoint();
        
        Transform bulletSpawnPoint;

        if(shootLeft){
            bulletSpawnPoint = leftBulletSpawnPoint;
            m_Animator.SetTrigger(basicAttackAnimationNames[0]);
            Debug.Log("SHOOTING LEFT");
        }
        else{
            Debug.Log("SHOOTING RIGHT");
            bulletSpawnPoint = rightBulletSpawnPoint;
            m_Animator.SetTrigger(basicAttackAnimationNames[1]);
        }

        Vector3 aimDir = (targetPoint - bulletSpawnPoint.position).normalized;
        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        
        //ApplyGunRecoil(aimDir);

        shootLeft = !shootLeft;
        Debug.Log(shootLeft);
    }

    public override void ExecuteAbility1() {
        Debug.Log("Guns Ability 1");
    }

    public override void ExecuteAbility2() {
        Debug.Log("Guns Ability 2");
    }

    Vector3 GetTargetPoint() {
        Vector3 targetPoint = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity)){
            targetPoint = raycastHit.point;
        }

        return targetPoint;
    }

    void ApplyGunRecoil(Vector3 aimDir) {
        aimDir.x = aimDir.x / Mathf.Abs(aimDir.x);
        aimDir.z = aimDir.z / Mathf.Abs(aimDir.z);
        aimDir.y = 0;

        aimDir = aimDir * recoil * -1;

        playerController.MovePlayer(aimDir);
    }
}
