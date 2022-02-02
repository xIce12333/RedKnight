using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing_Idle : StateMachineBehaviour
{
    private SlimeKing boss;         // boss reference   
    private float idleTimer;     // time before boss can do action



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<SlimeKing>();
        boss.GetComponent<EnemyHealth>().inVulnerable = false;
        boss.body.velocity = Vector2.zero;
        idleTimer = Random.Range(boss.minTime, boss.maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (idleTimer <= 0f)
            boss.RandomMovement();
        else
            idleTimer -= Time.deltaTime;    

        // boss faces player 0.3 second before next movement
        // ボスが次の動きをする0.3秒前、プレイヤーの方に顔を向けます
        if (idleTimer <= 0.3f) 
            boss.FlipBoss(); 

    }
}
