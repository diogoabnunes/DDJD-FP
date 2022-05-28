using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : WeaponController
{
    public GameObject player;
    PlayerController playerController;

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

        this.gameObject.SetActive(false);

        playerController = player.GetComponent<PlayerController>();
    }

    public override void Enable() {
        this.gameObject.SetActive(true);

        GunL.SetActive(true);
        GunR.SetActive(true);

        m_Animator.SetBool("hasGun", true);

        playerController.SetPlayerRotation(false);
    }

    public override void Disable() {
        playerController.SetPlayerRotation(true);

        m_Animator.SetBool("hasGun", false);

        GunL.SetActive(false);
        GunR.SetActive(false);

        this.gameObject.SetActive(false);
    }

    void Update() {
        // // bullet trajectory
        // Vector3 targetPoint = GetTargetPoint();
        // Vector3 aimDir = (targetPoint - bulletSpawnPoint.position).normalized;

        // // player rotation
        // Vector3 worldAimTarget = targetPoint;
        // worldAimTarget.y = player.transform.position.y;
        // Vector3 aimDirection = (worldAimTarget - player.transform.position).normalized;
        // player.transform.forward = Vector3.Lerp(player.transform.forward, aimDirection, Time.deltaTime * lerpRatio);
    }

    public override void ExecuteBasicAttack() {
        Debug.Log("Gun Attack");

        // bullet trajectory
        Vector3 targetPoint = GetTargetPoint();
        Vector3 aimDir = (targetPoint - bulletSpawnPoint.position).normalized;

        // player rotation
        Vector3 worldAimTarget = targetPoint;
        worldAimTarget.y = player.transform.position.y;
        Vector3 aimDirection = (worldAimTarget - player.transform.position).normalized;
        player.transform.forward = Vector3.Lerp(player.transform.forward, aimDirection, Time.deltaTime * lerpRatio);
        
        // transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
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
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f)){
            targetPoint = raycastHit.point;
        }

        return targetPoint;
    }
}
