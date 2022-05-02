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

    PlayerManager playerManager;
    Transform player;
    NavMeshAgent agent;

    float nextAttack = 0;

    protected void Start()
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
                Attack();
            }
        }
    }

    public virtual bool CanAttack(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        Debug.Log("Missing Implementation for: CanAttack()!");
        return false;
    }

    public virtual void Attack() {
        Debug.Log("Missing Implementation for: Attack()!");
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
        MoveTo(GetPlayerPosition());

        // play animation of moving
    }

    float ComputeDistanceToPlayer() {
        return Vector3.Distance(GetPlayerPosition(), GetEnemyPosition());
    }

    Quaternion ComputeRotationTowardsPlayer() {
        Vector3 direction = (GetPlayerPosition() - GetEnemyPosition()).normalized;
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
        agent.SetDestination(GetEnemyPosition());
    }

    public void AttackStarted() {
        isAttacking = true;
    }

    public virtual void AttackEnded() {
        isAttacking = false;
        Debug.Log("Attack Ended Enemy controller");
    }

    public Vector3 GetPlayerPosition() {
        return player.position;
    }

    public Vector3 GetEnemyPosition() {
        return transform.position;
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
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}
