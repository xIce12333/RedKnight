using UnityEngine;

public class PlayerAnimation : AnimationScript
{
    private PlayerMovement player;
    private PlayerHealth health;
    private PlayerAttack attack;


    public override void Start()
    {
        // Grab reference
        base.Start();
        player = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        attack = GetComponent<PlayerAttack>();
    }

   
    private void Update()
    {
        if (health.isDead) {
            ChangeAnimationState(State.Die);
            Invoke("PlayerDead", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    
        else if (attack.isAttacking) {
            ChangeAnimationState(State.Attack);
        }

        else if (player.isGrounded && !attack.isAttacking) {
            if (player.horizontalInput != 0 && !player.isDashing)
                ChangeAnimationState(State.Run);
            else if (!player.isDashing)
                ChangeAnimationState(State.Idle);
            else    
                ChangeAnimationState(State.Dash);
        }
        else if (player.isDashing)
            ChangeAnimationState(State.Dash);

        else if (!player.isGrounded && !attack.isAttacking)
            ChangeAnimationState(State.Jump);
        

    }

    private void PlayerDead()   // stop changing player animation after player death 　プレイヤーが死んだらアニメーションを止めます
    {
        SoundManager.instance.StopSound("BGM");
        SoundManager.instance.StopSound("Boss BGM");
        FindObjectOfType<RetryMenu>().ShowRetryMenu();        //　リトライメニューを表示します
        this.enabled = false;
    }
}
