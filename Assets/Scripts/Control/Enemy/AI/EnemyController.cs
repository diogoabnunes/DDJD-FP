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

    PlayerModel playerModel;
    Transform player;
    NavMeshAgent agent;

    protected GameManager gameManager;

    SpawnManager spawnManager = null;

    public Animator m_Animator;

    public virtual void Start()
    {
        playerModel = PlayerModel.instance;
        gameManager = GameManager.instance;
        player = PlayerModel.instance.player.transform;
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

    public Animator GetAnimator() {
      return m_Animator;
    }

    public bool isRunning() {
      Vector2 runningVector = new Vector2(agent.velocity.x, agent.velocity.z);
      float distance = Vector2.Distance(runningVector, Vector2.zero);

      if (distance > 1.0f) {
        return true;
      }

      return false;
    }

    protected bool PlayerInLookRange(float distance) {
        return distance <= lookRange;
    }

    float ComputeDistanceToPlayer() {
        return Vector3.Distance(GetPlayerPosition(), GetEnemyPosition());
    }

    public Quaternion ComputeRotationTowardsPlayer() {
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

    public void SetRigidbodyVelocity(Vector3 velocity) {
        GetComponent<Rigidbody>().velocity = velocity;
    }

    public void RemoveRigidbodyVelocity() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void DisableAI() {
        agent.enabled = false;
    }

    public void EnableAI() {
        agent.enabled = true;
    }

    public float GetStoppingDistance() {
        return agent.stoppingDistance;
    }

    public void SetStoppingDistance(float stoppingDistance) {
        agent.stoppingDistance = stoppingDistance;
    }

    public void GoTo(Vector3 destination) {
        agent.SetDestination(destination);
    }

    public void ShowDest() {
        Debug.Log(agent.destination);
    }

    public void ChasePlayer() {
        agent.SetDestination(player.position);
    }

    public void CancelMovement() {
        agent.SetDestination(transform.position);
    }

    public void StopMovement() {
        agent.isStopped = true;
    }

    public Vector3 GetPlayerPosition() {
        return player.position;
    }

    public Vector3 GetEnemyPosition() {
        return transform.position;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }

    void OnDestroy()
    {
        gameManager.addEnemyKilled();
        Debug.Log("Dead");
    }
}
