using UnityEngine;

public class RangedWeapon : MonoBehaviour
{

    public int damage = 10;
    public float range = 100f;

    public float fireRate = 30f;
    public float impactForce = 30f;

    private float nextTimeToFire = 0f;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }


    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, cam.transform.forward, out hit, range)){
            EnemyController target = hit.collider.GetComponent<EnemyController>();
            if(target != null){
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            Debug.Log(hit.collider.name);
            gameObject.GetComponent<ThirdPersonShooterController>().XP += hit.collider.GetComponent<EnemyController>().XPGiven();
            Debug.Log("XP Given: " + hit.collider.GetComponent<EnemyController>().XPGiven());
        }
    }
}
