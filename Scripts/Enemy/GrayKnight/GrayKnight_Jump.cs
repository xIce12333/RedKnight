using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayKnight_Jump : StateMachineBehaviour
{
    GrayKnight boss;
    private float jumpSpeed = 25f;
    private float jumpDuration = 0.8f;      // calculated by measurement
    private float timer;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 newPos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        boss = animator.GetComponent<GrayKnight>();
        FindRandomPos();
        boss.body.velocity = Vector2.zero;
        boss.Jump(jumpSpeed);
        boss.FlipBoss();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        float percentage = timer / jumpDuration;            //　"jumpDuration"　秒を使って特定の場所に移動します
        newPos = Vector2.Lerp(startPos, endPos, percentage);
        boss.transform.position = new Vector2(newPos.x, boss.transform.position.y);
        if (timer >= jumpDuration)
            animator.SetTrigger("idle");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    private void FindRandomPos()
    {
        startPos = boss.transform.position;
        float ran = Random.Range(boss.player.transform.position.x - 3, boss.player.transform.position.x + 3);
        ran = Mathf.Clamp(ran, boss.leftLimit.position.x, boss.rightLimit.position.x);
        endPos = new Vector2(ran, boss.transform.position.y);
    }

}
