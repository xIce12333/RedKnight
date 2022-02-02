using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_BackStep : StateMachineBehaviour
{
    GrayKnight boss;
    private float backStepDuration;     // = 0.2f
    private float backStepSpeed = 20f;
    private int playerDirection;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        backStepDuration = 0.2f;            // reset timer
        boss = animator.GetComponent<GrayKnight>();
        boss.body.velocity = Vector2.zero;
        boss.Jump(7f);
        playerDirection = boss.FindPlayerDirection();
        boss.FlipBoss();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (backStepDuration >= 0f)
            boss.body.velocity = new Vector2(-playerDirection * backStepSpeed, boss.body.velocity.y);
        else 
            animator.SetTrigger("idle");
        

        backStepDuration -= Time.deltaTime;
    }
}
