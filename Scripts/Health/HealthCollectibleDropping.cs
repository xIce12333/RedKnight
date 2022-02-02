using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectibleDropping : HealthCollectible
{   
    private int dropSpeed;        // random speed of 5 to 15
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        dropSpeed = Random.Range(5, 11);
        if (dropSpeed == 10)    // 　一定の確率でドロップしたHP回復薬の速度を超高速にし、ゲームをより刺激的にします。
            dropSpeed = 15; 
        body.velocity = new Vector2(body.velocity.x, -dropSpeed);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Ground") {
            Destroy(this.gameObject);
        }
    }
}
