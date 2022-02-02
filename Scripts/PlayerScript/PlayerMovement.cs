using UnityEngine;
using System.Collections;

public class PlayerMovement : Character
{
    #region Variables

    [Header("Player Scale")]
    private float PlayerScale = 1f;
    public float horizontalInput {get; private set;}
    private float jumpSpeed = 16f;

    [Header("Jumping")]
  //  private int jumpCount = 0;        // for double jump  ダブルジャンプに関する変数、いろいろ悩んだ結果、このゲームではダブルジャンプできないようにしました。
  //  private int extraJumps = 1;       
    private float jumpHangTime = 0.2f;
    private float jumpHangCounter;
    private float jumpCoolDown = 0.3f;
    private bool canJump = true;

    [Header("Wall Jumping")]
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    [Header("Wall Jump Force")]
    private float xWallJumpForce = 25f;
    private float yWallJumpForce = 10f;
    private float WalljumpDirection; 

    
    // For dashing
    [Header("Dashing")]
    private float dashSpeed = 35f;
    IEnumerator dashCoroutine;
    public bool isDashing {get; private set;} 
    private bool canDash = true;
    private float dashDirection;
    [System.NonSerialized] public float lastMirageXpos;        // for dash mirage　ダッシュ中の残像に関する変数
    
    #endregion

    #region  Functions
    public override void Awake()
    {
        // Grab references
        base.Awake();
    }
    private void Update()
    {
        // Get Player Horizontal Input  プレイヤー入力
        GetHorizontalInput();

        // Flip player left or right
        FlipPlayer();

        // Jump     
        CheckJump();
       
        // Check if player is on the ground
        CheckGrounded();

        // Check if player if sliding on the wall   壁ジャンプに関する関数
        CheckWallSliding();
        CheckWallJumping();

        // Dash     ダッシュに関する関数
        CheckDash();
    }

    private void FixedUpdate()      // modify physics related parameters　物理に関するパラメータを変える
    {
        modifyHorizontalSpeed();
        modifyGravity();
    }

    private void GetHorizontalInput()   
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");       // press A button = -1, D button = 1 
    }
    private void FlipPlayer()   //  プレイヤーが移動方向を変えるとき、キャラクターの向きを変える
    {
        if (!isWallJumping) {
            if (horizontalInput > 0.01f) 
                transform.localScale = new Vector2(PlayerScale, PlayerScale);
            else if (horizontalInput < -0.01f) 
                transform.localScale = new Vector2(-PlayerScale, PlayerScale);
        }

    }
    // horizontal speed     横移動の速度を変える
    private void modifyHorizontalSpeed()
    {
        if (!isDashing && !isWallJumping) {
            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
        }
        else if (isDashing) {
            body.velocity = new Vector2(dashDirection * dashSpeed, 0);
        }

    }
    // fall gravity and jump gravity    重力の影響を強調するために、落ちているときの重力を上げる
    private void modifyGravity()
    {
        if (!isDashing) {
            if (body.velocity.y < 0) {
                body.gravityScale = fallMultiplier;
            } 
            else {
                body.gravityScale = normalGravity;
            }
        }
    }  
    private void CheckJump() 
    {
    /*    if (Input.GetButtonDown("Jump")) {        // for double jump      ダブルジャンプ用
            if (isGrounded || jumpCount < extraJumps) {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
                jumpCount++;
            }
        } */
        if (isGrounded)
            jumpHangCounter = jumpHangTime;         // jumpHandCounter => add 0.2 seconds for player to jump even if player presses jump button too slow
        else                                        //  ゲーム体験を上げるために、プレイヤーがジャンプボタン押すのが少し遅れても、ジャンプできるようにしました
            jumpHangCounter -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && jumpHangCounter > 0f && canJump) {
            SoundManager.instance.PlaySound("Player Jump");
            StartCoroutine(JumpCoolDown());
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
            
    }

    private void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash == true) {
            if (dashCoroutine != null) {
                lastMirageXpos = transform.position.x;      // for dash mirage  ダッシュ残像の位置を変える、詳しくは Mirage Pool 及び Mirage のスクリプトにて
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = StartDash(0.1f, 0.3f);
            StartCoroutine(dashCoroutine);
        }
    }
    private IEnumerator StartDash(float dashDuration, float dashCooldown)
    {
        SoundManager.instance.PlaySound("Player Dash");
        isDashing = true;
        canDash = false;
        body.gravityScale = 0;          // disable gravity during dashing　　ダッシュ中は重力に影響されないようにします
        body.velocity = Vector2.zero;
        dashDirection = Mathf.Sign(transform.localScale.x);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public override void CheckGrounded() 
    {
        base.CheckGrounded();
  /*      if (isGrounded)       // for double jump 　ダブルジャンプ用
        {
            jumpCount = 0;
        } */
    }

    private IEnumerator JumpCoolDown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCoolDown);
        canJump = true;
    }

    private void CheckWallSliding()
    {
        if (OnWall() && horizontalInput != 0f) {
            if (body.velocity.y < -0.1f) 
                SoundManager.instance.PlaySoundIfNotPlaying("On Wall");
            isWallSliding = true;
        }
            
        else {
            isWallSliding = false;
            SoundManager.instance.StopSound("On Wall");
        }
    
        if (isWallSliding) 
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }

    private void CheckWallJumping()
    {
        if (isWallSliding && Input.GetButtonDown("Jump") && !isGrounded) {
            isWallJumping = true;
            SoundManager.instance.PlaySound("Player Jump");
            WalljumpDirection = -transform.localScale.x;
            transform.localScale = new Vector2(WalljumpDirection * PlayerScale, PlayerScale);
            Invoke("ResetWallJumping", 0.15f);      // wall jumping duration    壁ジャンプの長さ
        }

        if (isWallJumping) {
            body.velocity = new Vector2(xWallJumpForce * WalljumpDirection, yWallJumpForce);
        }
    }
    private void ResetWallJumping()
    {
        isWallJumping = false;
    }

    public void StopPlayerMovement()    // for dialogue     NPC会話中、プレイヤーの動きを止めるため、詳しくは DialogueManager　スクリプトにて
    {
        body.velocity = Vector2.zero;
    }
    
    #endregion

}
