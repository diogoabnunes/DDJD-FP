using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float rotationSpeed = 5f;

    public float health = 1f;

    public float lookRange = 10f;
    public float attackRange = 2.5f;

    public float attackDuration = 3f;

    public float attackDamage = 1f;

    private bool isAttacking = false;

    PlayerManager playerManager;
    Transform player;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        player = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking) {
            float distanceToPlayer = ComputeDistanceToPlayer();
            Quaternion rotationTowardsPlayer = ComputeRotationTowardsPlayer();
                
            Move(distanceToPlayer, rotationTowardsPlayer);

            if (CanAttack(distanceToPlayer, rotationTowardsPlayer)) {
                StartCoroutine(Attack());
            }
        }
    }

    bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return InAtackRange(distanceToPlayer) && IsFacingPlayer(rotationTowardsPlayer);
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
        agent.SetDestination(player.position);

        // play animation of moving
    }

    bool InAtackRange(float distance) {
        return distance <= attackRange;
    }

    IEnumerator Attack() {
        // stop movement
        agent.SetDestination(transform.position);
        
        isAttacking = true;

        // play animation of attack
        playerManager.TakeDamage(attackDamage);

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }

    float ComputeDistanceToPlayer() {
        return Vector3.Distance(player.position, transform.position);
    }

    Quaternion ComputeRotationTowardsPlayer() {
        Vector3 direction = (player.position - transform.position).normalized;
        return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    }

    bool IsFacingPlayer(Quaternion rotation) {
        return Quaternion.Angle(transform.rotation, rotation) == 0;
    }

    void FacePlayer(Quaternion rotation) {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
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
