using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float atkCooldownDuration = 1f;
    private float atkCooldown = 0f;
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
        }
    }
    public void StartPatrolling()
    {
        _animator.SetBool("Patroling", true);
    }
    public void StopPatrolling()
    {
        _animator.SetBool("Patroling", false);
    }
}
