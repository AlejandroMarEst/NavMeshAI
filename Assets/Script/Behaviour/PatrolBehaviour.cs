// PatrolBehaviour.cs
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float patrolRadius = 15f;
    [SerializeField] public float waypointReachedThreshold = 0.5f;

    public bool HasReachedDestination { get; private set; }

    bool isPatrolling;
    bool isMoving;
    Vector3 currentTarget;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StartPatrol()
    {
        isPatrolling = true;
        HasReachedDestination = false;
        PickNewPatrolPoint();
    }

    public void StopPatrol()
    {
        isPatrolling = false;
    }

    void Update()
    {
        if (!isPatrolling) return;
        HandlePatrolMovement();
    }

    void HandlePatrolMovement()
    {
        if (!isMoving) return;

        if (!agent.pathPending && agent.remainingDistance <= waypointReachedThreshold)
        {
            isMoving = false;
            isPatrolling = false;
            HasReachedDestination = true;
        }
    }

    void PickNewPatrolPoint()
    {
        if (!agent.isOnNavMesh) return;
        currentTarget = GetRandomNavMeshPoint();
        agent.SetDestination(currentTarget);
        isMoving = true;
    }

    Vector3 GetRandomNavMeshPoint()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDir = Random.insideUnitSphere * patrolRadius;
            randomDir += transform.position;
            if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
                return hit.position;
        }
        return transform.position;
    }
}