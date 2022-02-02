using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_Attack : StateMachineBehaviour
{
    GrayKnight boss;
    private int playerDirection;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<GrayKnight>();
        playerDirection = boss.FindPlayerDirection();
        boss.FlipBoss();
        boss.body.velocity = Vector2.zero;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss.isAttacking)
            boss.body.velocity = new Vector2(playerDirection * boss.attackSpeed, boss.body.velocity.y); 
    }

}
