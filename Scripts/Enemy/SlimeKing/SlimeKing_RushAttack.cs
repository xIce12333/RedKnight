using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing_RushAttack : StateMachineBehaviour
{
    private SlimeKing boss;
    private int direction;      // attack direction
    private float speed;
    private float timer;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<SlimeKing>();
        direction = boss.FindPlayerDirection();
        boss.FlipBoss();
        speed = boss.rushSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // gradually reduce the speed of rush attack, back to idle state when it drops below 0
        // ラッシュ攻撃の速度を徐々に下げて、0になったらidle状態に戻します。
        if (speed >= 0f && boss.isAttacking && !boss.OnWall(-1)) {
            boss.body.velocity = new Vector2(direction * speed, boss.body.velocity.y);
            speed -= Time.deltaTime * (10 + boss.stage * 2);
        }
        else if (speed <= 0.1f || boss.OnWall(-1)) {      // transit to idle if the boss touches the wall OR speed <= 0
            boss.isAttacking = false;
            animator.SetTrigger("idle");
        }
    }

}
