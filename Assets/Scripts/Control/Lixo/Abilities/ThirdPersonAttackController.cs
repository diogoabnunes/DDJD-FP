using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAttackController : MonoBehaviour
{

    [SerializeField] private Transform bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    public int damage = 10;

    public float fireRate = 30f;
    public float impactForce = 3f;

    bool hasGun = false;

    
    public Animator m_Animator;
    public GameObject Scythe;
    public GameObject GunL;
    public GameObject GunR;

    private float nextTimeToFire = 0f;

    public float lerpRatio = 20f;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator.SetBool("hasGun", false);
        Scythe.SetActive(true);
        GunL.SetActive(false);
        GunR.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f)){
            mouseWorldPosition = raycastHit.point;
        }


        if(Input.GetButton("Normal Attack") && hasGun == true && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;

            Vector3 worldAimTarget = mouseWorldPosition;
            
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * lerpRatio);

            
            transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            hasGun = !hasGun;
            if (hasGun == true)
            {
                Scythe.SetActive(false);
                GunL.SetActive(true);
                GunR.SetActive(true);
                m_Animator.SetBool("hasGun", true);
            }
        else
            {
                m_Animator.SetBool("hasGun", false);
                Scythe.SetActive(true);
                GunL.SetActive(false);
                GunR.SetActive(false);
            }
        }
        
    }


    void Shoot(){
        
    }
}
