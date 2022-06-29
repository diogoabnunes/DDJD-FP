using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float rotationSpeed = 5f;

    public float health = 1f;

    public float lookRange = 10f;

    float thresholdToNextMovement = 1f;

    bool locked = false;
    bool stopped = false;

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
        if (locked || stopped) return;

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

    public void Stop() {
        stopped = true;
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
        return Quaternion.Angle(transform.rotation, rotation) <= 5f;
    }

    public void FacePlayer(Quaternion rotation) {
        if (!IsFacingPlayer(rotation)) {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void SetAgentSpeed(float speed) {
        agent.speed = speed;
    }

    public float GetAgentSpeed() {
        return agent.speed;
    }

    public void AlternateAIAgent(int agentId) {
        agent.agentTypeID = agentId;
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

    public void ChasePlayer() {
        Vector3 previousDestination = agent.destination;
        Vector3 newDestination = player.position;
        Vector3 difference = newDestination - previousDestination;

        bool xDifference = Mathf.Abs(Mathf.Round(difference.x)) > thresholdToNextMovement;
        bool yDifference = Mathf.Abs(Mathf.Round(difference.y)) > thresholdToNextMovement;
        bool zDifference = Mathf.Abs(Mathf.Round(difference.z)) > thresholdToNextMovement;

        if (xDifference || yDifference || zDifference){
            agent.SetDestination(player.position);
        }
    }

    public void CancelMovement() {
        agent.SetDestination(transform.position);
    }

    public void StopMovement() {
        agent.isStopped = true;
    }

    public bool IsStopped() {
        return agent.isStopped;
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
}
