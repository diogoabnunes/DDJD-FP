using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float health = 1f;

    public float durationOfAttack = 3f;

    public float pursuitRange = 10f;
    public float attackRange = 2.5f;

    public float rotationSpeed = 5f;

    private bool isAttacking = false;

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
        if (CanPerformAction()) {
            PerformAction();
        }
    }

    // -------------------------------------

    bool CanPerformAction() {
        return !isAttacking;
    }

    void PerformAction() {
        float distanceToPlayer = ComputeDistanceToPlayer();
        Quaternion rotationTowardsPlayer = ComputeRotationTowardsPlayer();
            
        Move(distanceToPlayer, rotationTowardsPlayer);

        if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) {
            StartCoroutine(Attack());
        }
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return InAtackRange(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
    }

    void Move(float distanceToPlayer, Quaternion rotationTowardsPlayer) {        
        if(InPursuitRange(distanceToPlayer)) {
            Pursuit();
        }
        else {
            // play idle animation
        }

        if (!IsFacingPlayer(rotationTowardsPlayer)) {
            FacePlayer(rotationTowardsPlayer);
        }
    }

    bool InPursuitRange(float distance) {
        return distance <= pursuitRange;
    }

    void Pursuit() {
        agent.SetDestination(target.position);

        // play animation of moving
    }

    bool InAtackRange(float distance) {
        return distance <= attackRange;;
    }

    IEnumerator Attack() {
        // stop movement
        agent.SetDestination(transform.position);
        
        isAttacking = true;

        // play animation of attack

        yield return new WaitForSeconds(durationOfAttack);

        isAttacking = false;
    }

    float ComputeDistanceToPlayer() {
        return Vector3.Distance(target.position, transform.position);
    }

    Quaternion ComputeRotationTowardsPlayer() {
        Vector3 direction = (target.position - transform.position).normalized;
        return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    }

    bool IsFacingPlayer(Quaternion rotation) {
        return Quaternion.Angle(transform.rotation, rotation) == 0;
    }

    void FacePlayer(Quaternion rotation) {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        // play animation of rotation
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pursuitRange);
    }

    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log(health);

        //do something

        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
