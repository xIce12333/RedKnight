using UnityEngine;

public class Slime_backStep : StateMachineBehaviour
{
    SlimeMovement slime;
    private float timer;
    private float backStepDuration = 0.3f;
    private int backStepDirection;
    private float backStepSpeed = 10f;
    private float delay = 0.2f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        slime = animator.GetComponent<SlimeMovement>();
        slime.body.velocity = Vector2.zero;
        backStepDirection = slime.FindPlayerDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > delay && timer <= delay + backStepDuration)
            slime.body.velocity = new Vector2(-backStepDirection * backStepSpeed, slime.body.velocity.y); 
        else if (timer > delay + backStepDuration) 
            animator.SetTrigger("idle");
    }

}
