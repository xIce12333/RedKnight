using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawn : SlimeMovement
{
    // slime spawned by SlimeKing   スライムキングに召喚されたスライム
    private bool canAttack;
    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
        CheckGrounded();
        if (!canAttack && isGrounded) {
            canAttack = true;
            animator.SetTrigger("canAttack");
        }
        // ボスが　ﾁ───(´-ω-｀)───ﾝ　したら、召喚されたスライムを全部消します
        if (FindObjectOfType<SlimeKing>().GetComponent<EnemyHealth>().isDead)       // destroy spawn slime if boss is dead
            Destroy(this.gameObject);
    }
}
