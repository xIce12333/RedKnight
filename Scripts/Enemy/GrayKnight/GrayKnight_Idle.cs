using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_Idle : StateMachineBehaviour
{
    GrayKnight boss;
    private float idleTimer;
    private bool start;
    private float timer;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss = animator.GetComponent<GrayKnight>();
       idleTimer = Random.Range(boss.minTime, boss.maxTime);
       boss.body.velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (idleTimer <= 0.3f)      // boss faces player 0.3 second before next movement
             // ボスが次の動きをする0.3秒前、プレイヤーの方に顔を向けます
            boss.FlipBoss();
        boss.CheckGrounded();

        if (idleTimer <= 0)
            boss.RandomMovement();
        else
            idleTimer -= Time.deltaTime; 

        
    }
}
