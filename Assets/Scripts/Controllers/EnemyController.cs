using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public int health = 10;
    public float lookRadius = 10f;

    public float rotationSpeed = 5f;


    Transform target;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       float distance = Vector3.Distance(target.position, transform.position);

       if(distance <= lookRadius){
           agent.SetDestination(target.position);

           if(distance <= agent.stoppingDistance){
               FaceTarget();
           }
       }
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 ,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void TakeDamage(int damage){
        health -= damage;
        Debug.Log(health);

        //do something

        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
