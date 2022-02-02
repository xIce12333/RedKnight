using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing_Jump : StateMachineBehaviour
{
    private int horizontalSpeed;      // random speed value of 3 - 7
    private SlimeKing boss;
    private float timer;
    private int direction;      // jump direction
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<SlimeKing>();
        boss.FlipBoss();
        boss.body.velocity = Vector2.zero;
        timer = 0f;
        direction = boss.FindPlayerDirection();
        horizontalSpeed = Random.Range(3, 8);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        boss.CheckGrounded();
        if (timer >= 1f && boss.isGrounded) {
            
            boss.isAttacking = false;
            if (boss.stage != 1) {
                boss.ShakeCameraVertical();
                boss.PlayLandLoudSound();
            }
            else
                boss.PlayLandSound();
            
            animator.SetTrigger("idle");
        }
            
        if (boss.isAttacking)
            boss.body.velocity = new Vector2(direction * horizontalSpeed, boss.body.velocity.y);

    }
    
}
