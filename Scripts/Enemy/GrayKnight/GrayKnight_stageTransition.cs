using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_stageTransition : StateMachineBehaviour
{
    GrayKnight boss;    
    private float knockBackSpeed;   // max 25f
    private float knockBackDirection;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<GrayKnight>();
        boss.FlipBoss();
        knockBackSpeed = 25f;
        knockBackDirection = -boss.FindPlayerDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boss.OnWall(-1) && knockBackSpeed >= 0f) {     // speed can't be negative in this case     この場合速度はマイナスにならないようにします。
            SoundManager.instance.PlaySoundIfNotPlaying("SlideGround");
            boss.body.velocity = new Vector2(knockBackDirection * knockBackSpeed, 0f);     
            knockBackSpeed -= Time.deltaTime * 35;
        }
        else {
            boss.body.velocity = Vector2.zero;
            SoundManager.instance.StopSound("SlideGround");
            boss.GetComponent<Health>().inVulnerable = true;             // boss can't take damage when transiting
            animator.SetTrigger("enrage");
        }
    }
}
