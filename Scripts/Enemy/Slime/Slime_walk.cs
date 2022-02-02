using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_walk : StateMachineBehaviour
{
    SlimeMovement slime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       slime = animator.GetComponent<SlimeMovement>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FlipSide();
        slime.body.velocity = new Vector2(-slime.moveSpeed * Mathf.Sign(slime.transform.localScale.x), slime.body.velocity.y);
        if (slime.CanSeePlayer(slime.agroRange, -1)) 
            animator.SetTrigger("attack");
    }

    private void FlipSide()
    {
        if (slime.OnWall(-1) || slime.nearEdge()) {
            slime.transform.localScale = new Vector2(-slime.transform.localScale.x , slime.EnemyScale);
        }
    } 
}
