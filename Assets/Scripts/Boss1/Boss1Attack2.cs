using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attack2 : StateMachineBehaviour
{
    public int numMissiles = 10;
    public float missileDelay = .1f;
    public float pauseAfterMissiles = .5f;

    public GameObject missile;

    private float timer = 0f;
    private float changeStateTime;
    private float nextMissileTime;
    private int missilesFired = 0;
    private string trigger;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Boss1Attack2");
        timer = 0f;
        
        changeStateTime = missileDelay * numMissiles + pauseAfterMissiles;

        missilesFired = 0;
        nextMissileTime = missileDelay;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if(timer > changeStateTime) {
            changeState(animator);
        }

        if(missilesFired <= numMissiles && timer > nextMissileTime) {
            fireMissile(animator);
            missilesFired++;
            nextMissileTime += missileDelay;
        }

        timer += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //animator.ResetTrigger(trigger);
    }

    private void changeState(Animator animator) {
        var randNum = Random.Range(0, 3);
        
        if(randNum.Equals(0)) {
            changeState("DoMovement2", animator);
        }
        else {
            changeState("DoMovement1", animator);
        }
    }

    private void changeState(string state, Animator animator) {
        trigger = state;
        animator.SetTrigger(trigger);
    }

    private void fireMissile(Animator animator) {

        float angle = Random.Range(0, 360);
        
        var newMissile = Instantiate(missile, new Vector3(animator.transform.position.x, animator.transform.position.y, 1), Quaternion.Euler(0, 0, angle));
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
