using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing_RushPineapple : StateMachineBehaviour
{
    private SlimeKing boss;
    private int direction;      // attack direction
    private float speed;        // rush speed
    private bool shakedCamera;      // to ensure only shake camera once


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<SlimeKing>();
        direction = boss.FindPlayerDirection();
        boss.FlipBoss();
        shakedCamera = false;
        speed = boss.rushSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!boss.OnWall(-1) && boss.isAttacking) 
            boss.body.velocity = new Vector2(direction * speed, boss.body.velocity.y);
        else if (!shakedCamera && boss.OnWall(-1)) {
            boss.ShakeCameraHorizontal();           // ボスが壁にぶつけたので、カメラを振動させ、パイナップルトラップを発動させます。
            boss.isAttacking = false;
            shakedCamera = true;
            boss.ResetPineappleTimer();
            animator.SetTrigger("idle");
        }
        
    }

}
