using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidCoin : Coin
{
    //　敵を倒したときに出るコイン
    Rigidbody2D body;
    private float throwUpSpeed = 15f;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 3f;
        gameObject.SetActive(false);
    }

    public void ThrowUp()
    {
        body.velocity = new Vector2(0f, throwUpSpeed);
    }
}
