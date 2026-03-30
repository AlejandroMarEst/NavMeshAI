using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : MonoBehaviour
{
    [SerializeField] public Transform player;
    NavMeshAgent agent;
    bool isChasing = false;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (isChasing && player != null)
            agent.SetDestination(player.position);
    }
    public void StartChasing()
    {
        if (player == null) return;
        isChasing = true;
        agent.isStopped = false;
    }
    public void StopChasing()
    {
        isChasing = false;
        agent.isStopped = true;
        agent.ResetPath();
    }
}
