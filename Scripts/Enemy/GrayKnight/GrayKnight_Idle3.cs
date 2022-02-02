using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_Idle3 : StateMachineBehaviour
{
    GrayKnight boss;
    private float idleTimer;
    private int ran;
    private float halfTime;
    private float fullTime;
  
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<GrayKnight>();
        idleTimer = Random.Range(boss.minTime, boss.maxTime);
        ran = Random.Range(0, 3);       // for boss walking
        halfTime = idleTimer / 2;
        fullTime = idleTimer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.FlipBoss();    // boss always face player for stage 3  ３段階目のグレーナイトは、常にプレイヤーいる方向に顔を向けます。

        if (idleTimer <= fullTime) {
            if (ran == 0) {
                MoveTowardsPlayer();
            }
            else if (ran == 1) {
                MoveAwayfromPlayer();
            }
            else if (ran == 2) {
                MoveForwardBackward();
            }
        }

        if (idleTimer <= 0)
            boss.RandomMovement();
        else
            idleTimer -= Time.deltaTime;
        
    }
    
    private void MoveTowardsPlayer()
    {
        boss.body.velocity = new Vector2(boss.FindPlayerDirection() * boss.moveSpeed, boss.body.velocity.y);
    }
    private void MoveAwayfromPlayer()
    {
        boss.body.velocity = new Vector2(-boss.FindPlayerDirection() * boss.moveSpeed, boss.body.velocity.y);
    }
    private void MoveForwardBackward()      //　半分の時間を前、残り半分の時間を後ろに下がります。
    {
        if (idleTimer >= halfTime)
            MoveTowardsPlayer();
        else
            MoveAwayfromPlayer();
    }
}
