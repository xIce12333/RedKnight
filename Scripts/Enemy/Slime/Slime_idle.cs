using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_idle : StateMachineBehaviour
{
    SlimeMovement slime;
    private float timer;
    private float idleDuration = 0.8f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       slime = animator.GetComponent<SlimeMovement>();
       slime.body.velocity = Vector2.zero;
       timer = 0f;
       slime.FacePlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        slime.FacePlayer();
        timer += Time.deltaTime;
        if (timer >= idleDuration)
            animator.SetTrigger("attack");
    } 
}

