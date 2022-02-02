using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_health : EnemyHealth
{
    private Animator animator;
    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "PlayerAttack") 
            animator.SetTrigger("knockBack");
        
    }
}
