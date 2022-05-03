using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{

    [SerializeField] private Transform bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    public int damage = 10;

    public float fireRate = 30f;
    public float impactForce = 3f;

    private float nextTimeToFire = 0f;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
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



        if(Input.GetButton("Fire2") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }


    void Shoot(){
        
    }
}
