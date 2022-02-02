using UnityEngine;

public class Slime_knockBack : StateMachineBehaviour
{
    SlimeMovement slime;
    private float timer;
    private float duration = 0.1f;
    private float knockBackSpeed = 8f;
    private int direction;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       slime = animator.GetComponent<SlimeMovement>();
       slime.body.velocity = Vector2.zero;
       timer = 0f;
       slime.FacePlayer();
       direction = -slime.FindPlayerDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer <= duration) {
            slime.body.velocity = new Vector2(direction * knockBackSpeed, 0f);
        }
        else
            animator.SetTrigger("idle");
    }

}
