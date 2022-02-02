using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyDamage
{
    protected Animator animator;            
    [System.NonSerialized] public Transform player;     // player position  プレイヤーの位置
        
    [System.NonSerialized] public int stage = 1;        // Boss current stage   ボスの段階、ボスごとに3段階があります

    public override void Awake()
    {
       base.Awake();
       animator = GetComponent<Animator>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    public int FindPlayerDirection()
    {   
        return (player.position.x < transform.position.x) ? -1 : 1;
    }

    protected void modifyGravity()          // Boss gravity     ボスの重力
    {
        body.gravityScale = (body.velocity.y < 0) ? fallMultiplier : normalGravity;
    }

    public virtual void FlipBoss()
    {
        float scale = Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector2(-FindPlayerDirection() * scale, transform.localScale.y);
    }

    public void Jump(float jumpSpeed)
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
    }
}
