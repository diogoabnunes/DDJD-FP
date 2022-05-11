using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 10;
    public LayerMask m_LayerMask;

    private Collider[] hitColliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkCollisions();
        if(Input.GetButtonDown("Normal Attack")){
            foreach(Collider coll in hitColliders){
                coll.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                Debug.Log("Hit : " + coll.name);
                gameObject.GetComponent<ThirdPersonShooterController>().XP += coll.gameObject.GetComponent<EnemyController>().XPGiven();
                Debug.Log("XP Given: " + coll.gameObject.GetComponent<EnemyController>().XPGiven());
            }
        }
    }


    void checkCollisions(){
        hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(4,3,3)/2, Quaternion.identity, m_LayerMask);

        
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(4,3,3));
    }
}
