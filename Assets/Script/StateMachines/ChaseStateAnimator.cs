using UnityEngine;

public class ChaseStateAnimator : StateMachineBehaviour
{
    private EnemyController _enemyController;
    private ChaseBehaviour _chaseBehaviour;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyController = animator.GetComponent<EnemyController>();
        _chaseBehaviour = animator.GetComponent<ChaseBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyController.UpdateChase(_enemyController.IsPlayerInRange());
        if (_enemyController.onAtkRange)
            _chaseBehaviour.StopChasing();
        else 
            _chaseBehaviour.StartChasing();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chaseBehaviour.StopChasing();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
