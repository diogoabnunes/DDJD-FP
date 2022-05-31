using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float rotationSpeed = 5f;

    public float health = 1f;

    public float lookRange = 10f;

    bool locked = false;

    PlayerManager playerManager;
    Transform player;
    NavMeshAgent agent;

    SpawnManager spawnManager = null;

    public Animator m_Animator;

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
        ManageAnimations();
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

    public virtual void ManageAnimations() {
      // Function to manage animations
    }

    public Animator GetAnimator() {
      return m_Animator;
    }

    public bool isRunning() {
      Vector2 runningVector = new Vector2(agent.velocity.x, agent.velocity.z);

      if (!runningVector.Equals(Vector2.zero)) {
        Debug.Log("Enemy is Running!");
        return true;
      }

      Debug.Log("Enemy is Not Running!");
      return false;
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

    public void FacePlayer(Quaternion rotation) {
        if (!IsFacingPlayer(rotation)) {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void ChasePlayer() {
        //agent.Resume();
        agent.SetDestination(player.position);
    }

    public void StopMovement() {
        //agent.SetDestination(GetEnemyPosition());
        //agent.Stop();
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
        Debug.Log("hitten");

        if(health <= 0){
            Die();
        }
    }

    void Die() {
        if (spawnManager != null){
            spawnManager.enemyDied(this.gameObject);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}
