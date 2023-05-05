using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement2 : StateMachineBehaviour
{
    public float forwardSpeed = 6f;
    public float backwardSpeed = 3f;
    public float yBase = 3.75f;
    public float yForwardMax = -2f;
    private int direction = -1;
    private float currentSpeed;
    private string trigger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Boss1Movement2");
        direction = -1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentSpeed = direction > 0 ? backwardSpeed : forwardSpeed;

        animator.transform.position = animator.transform.position + (direction * Vector3.up) * currentSpeed * Time.deltaTime;

        if(animator.transform.position.y < yForwardMax) {
            direction = 1;
        }

        if(direction.Equals(1) && animator.transform.position.y >= yBase) {
            changeState(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //animator.ResetTrigger(trigger);
    }

    private void changeState(Animator animator) {
        var randNum = Random.Range(0, 4);
        
        if(randNum.Equals(0)) {
            changeState("DoAttack1", animator);
        }
        else if(randNum.Equals(1)) {
            changeState("DoAttack2", animator);
        }
        else {
            changeState("DoMovement1", animator);
        }
    }

    private void changeState(string state, Animator animator) {
        trigger = state;
        animator.SetTrigger(trigger);
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
