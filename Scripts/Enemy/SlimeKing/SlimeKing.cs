using UnityEngine;
using System.Collections;

public class SlimeKing : Boss
{

    #region Variables 変数
    // references

    [SerializeField] private GameObject[] waterBalls;         // waterBall object pooling

    [Header("References")]
    [SerializeField] private Transform leftWall;            // leftWall position
    [SerializeField] private Transform rightWall;           // rightWall position
    [SerializeField] private Dialogue endBattleDialogue;
    private EnemyHealth health;

    // variables
    [System.NonSerialized] public float minTime = 1.5f;            // min idle time (stage 1, 2), 1f for stage 3
    [System.NonSerialized] public float maxTime = 2f;              // max idle time (stage 1, 2), 1.5f for stage 3
    [System.NonSerialized] public bool isAttacking;
    public float rushSpeed {get; private set;}             // rush speed : 13f (stage 1), 15f (stage 2), 17f (stage 3)
    [System.NonSerialized] public float rushPineappleTimer;                 // for checking if boss can do "rushpineapple attack"
    [System.NonSerialized] public float rushPineappleResetTime = 12f;           // reset time of timer

    private float waterBallHorizontalSpeed = 10f;         // speed of horizontal waterBall attack
    private float waterBallProjectileSpeed = 15f;         // speed of projectile waterBall attack

    #endregion

    #region  Functions 関数
    public override void Awake()
    {
        base.Awake();
        rushPineappleTimer = rushPineappleResetTime;
        rushSpeed = 13f;
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        modifyGravity();
        rushPineappleTimer -= Time.deltaTime;
        if (health.isDead && stage != -1) {     // stage = -1 to ensure that the function is called only once
            EndBattle();
        }
        else if ( (health.GetCurrentHealth() <= health.GetStartingHealth() * 2/3)  && stage == 1)
            ToStageTwo();
        else if ( (health.GetCurrentHealth() <= health.GetStartingHealth() * 1/3)  && stage == 2)
            ToStageThree();
 
    }

    #region Boss Stage Parameters 段階別にボスのパラメータを変えます。
    private void ToStageTwo()           // change boss parameters for stage two 
    {
        SoundManager.instance.PlaySound("Enemy Hurt");
        stage = 2;
        fallMultiplier = 5f;
        rushSpeed = 15f;
        animator.SetTrigger("stageTwo");
    }

    private void ToStageThree()         // change boss parameters for stage three
    {
        SoundManager.instance.PlaySound("Enemy Hurt");
        stage = 3;
        fallMultiplier = 8f;
        rushSpeed = 17f;
        minTime = 1f;
        maxTime = 1.5f;
        animator.SetTrigger("stageThree");
    }
    private void EndBattle()
    {
        stage = -1;
        animator.SetTrigger("endBattle");
        FindObjectOfType<DialogueManager>().StartDialogue(endBattleDialogue);
    }
    #endregion

    private void ShootWaterBall()
    {
        // ボスが１段階目の場合、水平の水玉攻撃しかしません
        if (stage == 1)
            waterBalls[FindWaterBall()].GetComponent<WaterBall>().StartAttack(true, 0f, transform.localScale.x, waterBallHorizontalSpeed);
        else {
            int ran = Random.Range(0, 2);
            // ボスが２、３段階目の場合、1/2の確率で水平の水玉を撃ちます
            if (ran == 0)           // 1/2 probabiliy, shoot horizontal
                // horizontalspeed increases with stage 　ボスの段階が高いほど、水平の水玉の速度が上がります
                waterBalls[FindWaterBall()].GetComponent<WaterBall>().StartAttack(true, 0f, transform.localScale.x, waterBallHorizontalSpeed + stage);
            else  { 
                float angle = Random.Range(30f, 75f);      // random angle for projectile waterBall　水玉のアングルをランダムで選びます
                waterBalls[FindWaterBall()].GetComponent<WaterBall>().StartAttack(false, angle, transform.localScale.x, waterBallProjectileSpeed);
            } 
    
        }
    }

    private int FindWaterBall()     // for object pooling   空いてる（activeではない）水玉を探します
    {
        for (int i = 0; i < waterBalls.Length; i++) {
            if (!waterBalls[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }

    private void SpawnSlime()      // for stage 2 and 3     スライムを召喚します
    {
        if (stage == 2)
            GetComponent<SlimeSpawner>().StartSpawn(0.1f, 0.05f, 5);
        else if (stage == 3) {
            int ran = Random.Range(0, 3);
            // ボスが３段階目の場合、1/3の確率で２つのスライムを召喚します
            if (ran == 0)       // 1/3 probability, spawn two slime
                GetComponent<SlimeSpawner>().StartSpawn(0.1f, 0.15f, 5);     
            else    // 2/3 probability, spawn one slime
                GetComponent<SlimeSpawner>().StartSpawn(0.1f, 0.05f, 5);
        }
            
    }

    #region Boss boolean conditions

    private bool inJumpRange()
    {
        if (Mathf.Abs(transform.position.x - leftWall.position.x) <= 5f && transform.localScale.x > 0f) 
            return false;
        else if (Mathf.Abs(transform.position.x - rightWall.position.x) <= 5f && transform.localScale.x < 0f)
            return false;
        return true; 
    }

    private bool playerNearBoss()
    {
        return Mathf.Abs(transform.position.x - player.position.x) <= 2f;
    }

    private bool canBackStep()
    {
        if (Mathf.Abs(transform.position.x - leftWall.position.x) <= 3f && transform.localScale.x < 0f) 
            return false;
        else if (Mathf.Abs(transform.position.x - rightWall.position.x) <= 3f && transform.localScale.x > 0f)
            return false;
        return true;
    }

    public override bool OnWall(int originalDirection = 1)              // check if the boss touches either of the wall (GroundLayer)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, new Vector2( originalDirection * transform.localScale.x, 0), 0.1f, GroundLayer);
        return raycastHit.collider != null; 
    }

    private bool canShootWaterBall()        // for stage 2 and stage 3, player can't escape
    {
        return inJumpRange();
    }

    #endregion
    

    private void SetIsAttacking()       // for animation events : jump, rush
    {
        isAttacking = true;
    }


    #region  Camera Shaking
    public void ShakeCameraHorizontal()     // camera shaking for rushPineapple
    {
        StartCoroutine(FindObjectOfType<CameraShake>().ShakeHorizontal(0.8f, 1f));
        Invoke("SpawnPineapple", 1.5f);
    }

    public void ShakeCameraVertical()       // camera shaking for stage 2, 3 jumping
    {
        StartCoroutine(FindObjectOfType<CameraShake>().ShakeVertical(0.1f, 0.5f));
        Invoke("SpawnSlime", 0.3f);
    }
    #endregion

    private void SpawnPineapple()       
    {
        FindObjectOfType<PineappleSpawner>().StartSpawn();
    }

    public void ResetPineappleTimer()
    {
        rushPineappleTimer = rushPineappleResetTime;
    }

    private IEnumerator ChangeColorCoroutine()    //  for stage transition animation event
    {
        for (int i = 0; i < 8; i++) {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
        animator.SetTrigger("startBattle");
    }

    #region  Boss Logic 

    // public for state machine behavior
    public void RandomMovement()        // choose random movement 
    {
        if (stage == 1)
            StageOneMovement();
        else if (stage == 2)
            StageTwoMovement();
        else
            StageThreeMovement();
    }

    private void StageOneMovement()         // stage one movement
    {
        bool action = false;
        do {
            int ran = Random.Range(0, 4);   // 0 to 3
            if (rushPineappleTimer <= 0f) {
                animator.SetTrigger("rushPineapple");
                action = true;
            }
            // random movement
            else if (ran == 0) {
                animator.SetTrigger("rushAttack");
                action = true;
            }
            else if (ran == 1 && inJumpRange()) {
                animator.SetTrigger("jump");
                action = true;
            }
            else if (ran == 2) {
                animator.SetTrigger("waterAttack");
                action = true;
            }
            else if (ran == 3) {
                animator.SetTrigger("doubleWaterAttack");
                action = true;
            }
        } while (!action); 
     
    }
    private void StageTwoMovement()         // stage two movement
    {
        bool action = false;
        do {
            int ran = Random.Range(0, 8);   // value of 0 to 7
            if (rushPineappleTimer <= 0f) {
                animator.SetTrigger("rushPineapple");
                action = true;
            }
            // random movement
            else if (ran == 0) {
                animator.SetTrigger("rushAttack");
                action = true;
            }
            else if ((ran == 1 || ran == 2) && inJumpRange()) {
                animator.SetTrigger("jump");
                action = true;
            }
            else if (ran == 3 && canShootWaterBall()) {
                animator.SetTrigger("waterAttack");
                action = true;
            }
            else if (ran == 4 && canShootWaterBall()) {
                animator.SetTrigger("doubleWaterAttack");
                action = true;
            }
            // ran = 5 to 7
            else if (canBackStep() && playerNearBoss()){  
                animator.SetTrigger("backStep");
                action = true;
            }

        } while (!action); 
    }

    private void StageThreeMovement()       // stage three movement
    {
        bool action = false;        // to ensure that this function is called only once
        do {
            int ran = Random.Range(0, 12);   // value of 0 to 11
            if (rushPineappleTimer <= 0f) {
                animator.SetTrigger("rushPineapple");
                action = true;
            }
            // random movement
            else if (ran == 0) {
                animator.SetTrigger("rushAttack");
                action = true;
            }
            else if ((ran == 1 || ran == 2) && inJumpRange()) {
                animator.SetTrigger("jump");
                action = true;
            }
            else if (ran == 3 && canShootWaterBall()) {
                animator.SetTrigger("waterAttack");
                action = true;
            }
            else if (ran == 4 && canShootWaterBall()) {
                animator.SetTrigger("doubleWaterAttack");
                action = true;
            }
            else if (ran == 5 && canShootWaterBall()) {
                animator.SetTrigger("tripleWaterAttack");
                action = true;                
            }
            // ran = 6 to 11, 1/2 probabiliy to backstep
            //　３段階目のボスは回避（バックステップ）を優先します（1/2の確率）
            else if (canBackStep()){  
                animator.SetTrigger("backStep");
                action = true;
            }

        } while (!action);         
    }
    #endregion

    #region  SoundEffect　サウンドエフェクト

    public void PlayRushSound()
    {

        SoundManager.instance.PlaySound("Slime Rush");
    }

    public void PlayJumpSound()
    {
        SoundManager.instance.PlaySound("Slime Jump");
    }

    public void PlayLandSound()
    {
        SoundManager.instance.PlaySound("SlimeKing Land");
    }

    public void PlayWaterBallSound()
    {
        SoundManager.instance.PlaySound("WaterBall Attack");
    }

    public void PlayLandLoudSound()
    {
        SoundManager.instance.PlaySound("SlimeKing LandLoud");
    }

    public void PlayRoarSound()
    {
        SoundManager.instance.PlaySound("Roar");
    }
    #endregion

    #endregion
}
