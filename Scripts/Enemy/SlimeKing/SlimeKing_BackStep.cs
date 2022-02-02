using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing_BackStep : StateMachineBehaviour
{
    private SlimeKing boss;
    private float duration;  // backStep duration = 1f
    private int direction; // backStep direction
    private float backStepSpeed = 12f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<SlimeKing>();
        boss.FlipBoss();
        boss.body.velocity = Vector2.zero;
        duration = 1f;
        direction = -boss.FindPlayerDirection();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (duration <= 0f || boss.OnWall(-1)) {
            boss.isAttacking = false;
            animator.SetTrigger("idle");
        }
        else {
            duration -= Time.deltaTime;
        }
        if (boss.isAttacking)
            boss.body.velocity = new Vector2(backStepSpeed * direction, boss.body.velocity.y);
    }

    
}
