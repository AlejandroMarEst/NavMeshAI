using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float atkCooldownDuration = 1f;
    [SerializeField] public Transform _player;
    [SerializeField] public float chaseRange;
    private float atkCooldown = 0f;
    public bool onAtkRange;
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (atkCooldown > 0f)
            atkCooldown -= Time.deltaTime;
    }
    private void OnTriggerStay(Collider trigger)
    {
        if (atkCooldown <= 0f && trigger.CompareTag("Player"))
        {
            _animator.SetTrigger("Attack");
            atkCooldown = atkCooldownDuration;
            onAtkRange = true;
            trigger.GetComponent<Player>().TakeDMG();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        onAtkRange = false;
    }
    public void StartPatrolling()
    {
        _animator.SetBool("Patroling", true);
    }
    public void StopPatrolling()
    {
        _animator.SetBool("Patroling", false);
    }
    public void StartChase()
    {
        _animator.SetBool("Chasing", true);
    }
    public void UpdateChase(bool chase)
    {
        _animator.SetBool("Chasing", chase);
    }
    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, _player.transform.position) <= chaseRange;
    }
}
