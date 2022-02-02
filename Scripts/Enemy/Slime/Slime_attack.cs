using UnityEngine;

public class Slime_attack : StateMachineBehaviour
{
    SlimeMovement slime;
    private float timer;
    private float attackDuration = 1f;
    private float attackDelay = 0.2f;
    private float attackSpeed = 8f;
    private int attackDirection;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        slime = animator.GetComponent<SlimeMovement>();
        slime.body.velocity = Vector2.zero;
        slime.FacePlayer();
        attackDirection = slime.FindPlayerDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (slime.OnWall(-1) || timer > attackDelay + attackDuration || slime.nearEdge()) 
            animator.SetTrigger("idle");
        else if (timer >= attackDelay && timer <= attackDelay + attackDuration) 
            slime.body.velocity = new Vector2(attackDirection * attackSpeed, 0);
    }  
}
