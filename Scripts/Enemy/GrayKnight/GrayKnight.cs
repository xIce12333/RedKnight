using System.Collections;
using UnityEngine;

public class GrayKnight : Boss
{
    #region  Variables 変数
    private bool isFlipped = false; 
    [SerializeField] private Dialogue endBattleDialogue;
    private GrayKnightHealth health;
    [Header("Reference")]
    public Transform leftLimit;
    public Transform rightLimit;
    [Header("Boss parameters")]
    [System.NonSerialized] public float attackSpeed = 20f;         //  20 for stage 1, 22 for stage 2, 24 for stage 3
    [System.NonSerialized] public float dashAttackSpeed = 35f;      // 35 for stage 1, 38 for stage 2, 40 for stage 3
    [System.NonSerialized] public bool isAttacking;

    [System.NonSerialized] public float minTime = 1f;            // min idle time (stage 1, 2), 0.8f for stage 3  
    [System.NonSerialized] public float maxTime = 1.5f;              // max idle time (stage 1), 1.2f for stage 2, 1f for stage 3
    #endregion

    #region  Functions 関数
    public override void Awake()
    {
        base.Awake();
        health = GetComponent<GrayKnightHealth>();
    }

    private void Update()
    {
        modifyGravity();   // from base class
        CheckGrounded();
        if (health.GetCurrentHealth() <= 40 && stage == 1) {
            ToStageTwo();
        }
        else if (health.GetCurrentHealth() <= 20 && stage == 2) {
            ToStageThree();
        }
        else if (health.isDead && stage != -1) {
            EndBattle();
        }
    }

    #region Boss Parameters and End Battle Dialogue     ボス段階別のパラメータ調整及び会話

    private void ToStageTwo()
    {
        SoundManager.instance.PlaySound("Enemy Hurt");
        body.velocity = Vector2.zero;
        stage = 2;
        maxTime = 1.2f;
        attackSpeed = 22f;
        dashAttackSpeed = 38f;
        animator.SetTrigger("stageTwo");
    }
    private void ToStageThree()
    {
        SoundManager.instance.PlaySound("Enemy Hurt");
        body.velocity = Vector2.zero;
        stage = 3;
        minTime = 0.8f;
        maxTime = 1f;
        attackSpeed = 24f;
        dashAttackSpeed = 40f;
        animator.SetTrigger("stageThree");
    }

    private void EndBattle()
    {
        stage = -1;
        body.velocity = Vector2.zero;
        FindObjectOfType<DialogueManager>().StartDialogue(endBattleDialogue);
        animator.SetTrigger("endBattle");
    }

    #endregion

    #region Functions for animator events
    public void SetIsAttacking()        // for animator event
    {
        isAttacking = true;
    }
    public void SetIsAttackingFalse()
    {
        isAttacking = false;
        body.velocity = Vector2.zero;       // stop inertia     ボスが攻撃を終えたら、慣性を止めるために速度をリセットします
    }

    public void SetInvulnerability()        // set to false for stage transition    
    {
        GetComponent<Health>().inVulnerable = false;
    }

    #endregion
    public override void FlipBoss()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }


    #region Boss Logic

    // public for state machine script
    public void RandomMovement()
    {
        if (stage == 1)
            StageOneMovement();
        else if (stage == 2)
            StageTwoMovement();
        else
            StageThreeMovement();

    }

    private void StageOneMovement()
    {
        bool action = false;
        do {
            int ran = Random.Range(0, 4);
            if (ran == 0 && inAttackRange()) {
                animator.SetTrigger("attack");
                action = true;
            }
            else if (ran == 1) {
                animator.SetTrigger("dashAttack");
                action = true;
            }
            else if (ran == 2 && inBackStepRange() && inAttackRange()) {
                animator.SetTrigger("backStep");
                action = true;
            }
            else if (ran == 3) {
                animator.SetTrigger("jump");
                action = true;
            }
        } while (!action);
    }

    private void StageTwoMovement()
    {
        bool action = false;
        do {
            int ran = Random.Range(0, 9);
            if (ran == 0 && inAttackRange()) {
                animator.SetTrigger("attack");
                action = true;
            }
            else if (ran == 1) {
                animator.SetTrigger("dashAttack");
                action = true;
            }
            else if (ran == 2) {
                animator.SetTrigger("doubleAttack");
                action = true;
            }
            else if ((ran == 3 || ran == 7 || ran == 8) && inBackStepRange() && inAttackRange()) {
                animator.SetTrigger("backStep");
                action = true;
            }
            else if (ran == 4 && inBackStepRange()) {
                animator.SetTrigger("backStepAttack");
                action = true;
            }
            else if (ran == 5) {
                animator.SetTrigger("doubleDashAttack"); 
                action = true; 
            }
            else if (ran == 6) {
                animator.SetTrigger("jump");
                action = true;
            }
        } while (!action);
    }
    private void StageThreeMovement()
    {
        body.velocity = Vector2.zero;   // stop the inertia　慣性を止めます
        bool action = false;
        do {
            if (inAttackRange() && inBackStepRange()) {     //　３段階目のボスは、バックステップしてプレイヤーの攻撃を避けることを優先します。
                int rand = Random.Range(0, 6);
                if (rand == 0) {
                    animator.SetTrigger("backStep");
                    break;
                }
                else if (rand == 1) {
                    animator.SetTrigger("backStepDrop");
                    break; 
                }  
                else if (rand == 2) {
                    animator.SetTrigger("backStepAttack");
                    break;
                }
            }
            int ran = Random.Range(0, 7);
            if (ran == 0) {
                animator.SetTrigger("dropAttack");
                action = true;
            }
            else if (ran == 1) {
                animator.SetTrigger("dropDoubleAttack");
                action = true;
            } 
            else if (ran == 2 && inBackStepRange()) {
                animator.SetTrigger("backStep");
                action = true;
            }
            else if (ran == 3) {
                animator.SetTrigger("jump");
                action = true;
            }
            else if (ran == 4 && inBackStepRange()) {
                animator.SetTrigger("backStepAttack");
                action = true;
            }
            else if (ran == 5) {
                animator.SetTrigger("doubleAttack"); 
                action = true; 
            }
            else if (ran == 6 && inBackStepRange()) {
                animator.SetTrigger("backStepDrop");
                action = true;
            } 
        } while (!action);
    }


    #endregion

    #region Boss boolean conditions
    public bool inAttackRange()
    {
        return Mathf.Abs(player.transform.position.x - transform.position.x) <= 4f;
    }

    public bool inBackStepRange()
    {
        if (transform.position.x >= leftLimit.position.x && transform.position.x <= rightLimit.position.x)
            return true;
        else if (transform.position.x < leftLimit.position.x && FindPlayerDirection() == -1)
            return true;
        else if (transform.position.x > rightLimit.position.x && FindPlayerDirection() == 1)
            return true;
        return false;
    }
    #endregion

    #region Sound Effects
    // public for animator events
    public void SwordSound()
    {
        SoundManager.instance.PlaySound("Enemy Sword");
    }
    public void JumpSound()
    {
        SoundManager.instance.PlaySound("Enemy Jump");
    }
    public void SwordDropSound()
    {
        SoundManager.instance.PlaySound("Enemy SwordDrop");
    }
    public void LandSound()
    {
        SoundManager.instance.PlaySound("Enemy Land");
    }

    public void RoarSound()
    {
        SoundManager.instance.PlaySound("Roar");
    }
    #endregion

    #endregion
}
