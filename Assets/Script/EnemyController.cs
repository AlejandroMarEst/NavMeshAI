using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float atkCooldownDuration = 1f;
    [SerializeField] public Transform _player;
    [SerializeField] public float chaseRange;
    private float atkCooldown = 0f;
    private float hp = 3;
    public bool onAtkRange;
    private Animator _animator;
    private bool alive = true;
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
        if (atkCooldown <= 0f && trigger.CompareTag("Player") && alive)
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
    public void DamageEnemy()
    {
        --hp;
        if (hp <= 0 && alive)
        {
            _animator.Play("Dying");
            alive = false;
        }
    }
}
