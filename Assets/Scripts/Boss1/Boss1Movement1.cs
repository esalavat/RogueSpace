using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement1 : StateMachineBehaviour
{
    public float speed = 2f;
    public float xBounds = 3f;
    private int direction = 1;
    private float timer = 0f;
    private float minTime = 1f;
    private float maxTime = 3f;
    private string trigger;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Boss1Movement1");
        timer = Random.Range(minTime, maxTime);
        direction = Random.Range(0,2).Equals(0) ? 1 : -1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.transform.position = animator.transform.position + (direction * Vector3.right) * speed * Time.deltaTime;

        if(animator.transform.position.x > xBounds) {
            direction = -1;
        } else if(animator.transform.position.x < (-1 * xBounds)) {
            direction = 1;
        }

        if(timer <= 0) {
            changeState(animator);
        }

        timer -= Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger(trigger);
    }

    private void changeState(Animator animator) {
        var randNum = Random.Range(0, 6);
        
        if(randNum.Equals(0)) {
            changeState("DoAttack1", animator);
        }
        else if(randNum.Equals(1)) {
            changeState("DoAttack2", animator);
        }
        else {
            changeState("DoMovement2", animator);
        }
    }

    private void changeState(string state, Animator animator) {
        trigger = state;
        animator.SetTrigger(state);
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
