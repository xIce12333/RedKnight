using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_DropAttack : StateMachineBehaviour
{
    GrayKnight boss;
    private float timer;
    private float jumpSpeed = 28f;
    private float jumpDuration = 0.45f;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 newPos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<GrayKnight>();
        timer = 0f;
        boss.body.velocity = Vector2.zero;
        boss.Jump(jumpSpeed);
        boss.FlipBoss();
        FindAttackPos();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer <= jumpDuration) {
            float percentage = timer / jumpDuration;
            newPos = Vector2.Lerp(startPos, endPos, percentage);
            boss.transform.position = new Vector2(newPos.x, boss.transform.position.y);
        }
    }



    private void FindAttackPos()
    {
        float playerXPos = Mathf.Clamp(boss.player.transform.position.x, boss.leftLimit.position.x, boss.rightLimit.position.x);
        startPos = boss.transform.position;
        endPos = new Vector2(playerXPos, boss.transform.position.y);
    }

    
}
