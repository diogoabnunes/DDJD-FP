using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float rotationSpeed = 5f;

    public float health = 1f;

    public float lookRange = 10f;

    protected bool isAttacking = false;

    protected PlayerManager playerManager;
    protected Transform player;
    NavMeshAgent agent;

    void Start()
    {
        playerManager = PlayerManager.instance;
        player = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isAttacking) {
            float distanceToPlayer = ComputeDistanceToPlayer();
            Quaternion rotationTowardsPlayer = ComputeRotationTowardsPlayer();
                
            Move(distanceToPlayer, rotationTowardsPlayer);

            if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) {
                Debug.Log("Attack!");
                StartCoroutine(Attack());
            }
        }
    }

    public virtual bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        Debug.Log("Missing Implementation for: CanAttack()!");
        return true;
    }

    void Move(float distanceToPlayer, Quaternion rotationTowardsPlayer) {        
        if(PlayerInLookRange(distanceToPlayer)) {
            ChasePlayer();
            
            if (!IsFacingPlayer(rotationTowardsPlayer)) {
                FacePlayer(rotationTowardsPlayer);
            }
        }
    }

    bool PlayerInLookRange(float distance) {
        return distance <= lookRange;
    }

    void ChasePlayer() {
        MoveTo(player.position);

        // play animation of moving
    }

    public virtual IEnumerator Attack() {
        Debug.Log("Missing Implementation for: Attack()!");
        yield return null;
    }

    float ComputeDistanceToPlayer() {
        return Vector3.Distance(player.position, transform.position);
    }

    Quaternion ComputeRotationTowardsPlayer() {
        Vector3 direction = (player.position - transform.position).normalized;
        return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    }

    public bool IsFacingPlayer(Quaternion rotation) {
        return Quaternion.Angle(transform.rotation, rotation) == 0;
    }

    void FacePlayer(Quaternion rotation) {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    public void MoveTo(Vector3 position) {
        agent.SetDestination(position);
    }

    public void StopMovement() {
        agent.SetDestination(transform.position);
    }

    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log(health);

        //do something

        if(health <= 0){
            Die();
        }
    }

    void Die() {
        Debug.Log("Dying!");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}
