using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing_KnockBack : StateMachineBehaviour
{
    private SlimeKing boss;
    private int direction; // knockback direction
    private float knockBackSpeed;       // max 25f

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knockBackSpeed = 25f;
        boss = animator.GetComponent<SlimeKing>();
        boss.FlipBoss();
        boss.body.velocity = Vector2.zero;
        direction = -boss.FindPlayerDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boss.OnWall(-1) && knockBackSpeed >= 0f) {     // speed can't be negative in this case
            SoundManager.instance.PlaySoundIfNotPlaying("SlideGround");
            boss.body.velocity = new Vector2(direction * knockBackSpeed, 0f);     
            knockBackSpeed -= Time.deltaTime * 35;
        }
        else {
            boss.body.velocity = Vector2.zero;
            SoundManager.instance.StopSound("SlideGround");
            boss.GetComponent<Health>().inVulnerable = true;             // boss can't take damage when transiting
            animator.SetTrigger("stageTransition");
        }
    }

}
