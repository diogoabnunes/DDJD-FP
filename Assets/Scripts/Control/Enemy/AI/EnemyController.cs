using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float rotationSpeed = 5f;

    public float health = 1f;

    public float lookRange = 10f;

    // protected bool isAttacking = false;
    bool locked = false;

    PlayerManager playerManager;
    Transform player;
    protected NavMeshAgent agent;

    float nextAttack = 0;

    SpawnManager spawnManager = null;

    protected void Start()
    {
        playerManager = PlayerManager.instance;
        player = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        SpawnManager[] obj = FindObjectsOfType<SpawnManager>();
        if (obj.Length != 0) {
            spawnManager = obj[0];
        }
    }

    void Update() {
        if (locked) return;

        float distanceToPlayer = ComputeDistanceToPlayer();
        Quaternion rotationTowardsPlayer = ComputeRotationTowardsPlayer();

        Action action = GetNextAction(distanceToPlayer, rotationTowardsPlayer);
        if (action != null) {
            action.execute();
        }
    }

    public virtual Action GetNextAction(float distanceToPlayer, Quaternion rotationTowardsPlayer) {
        return null;
    }

    public void Lock() {
        locked = true;
    }

    public void Unlock() {
        locked = false;
    }
    protected bool PlayerInLookRange(float distance) {
        return distance <= lookRange;
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
        if (!IsFacingPlayer(rotation)) {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void StopMovement() {
        agent.SetDestination(GetEnemyPosition());
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
        if (spawnManager == null){
            spawnManager.enemyDied(this.gameObject);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}
