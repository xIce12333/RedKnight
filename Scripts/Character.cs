using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] public float moveSpeed = 7f;

    [Header("LayerMask")]
    protected LayerMask GroundLayer;
    protected LayerMask WallLayer;

    [Header("Gravity")]
    [SerializeField] protected float fallMultiplier = 7f;
    [SerializeField] protected float normalGravity = 5f;

    [Header("Rigidbody")]
    [System.NonSerialized] public Rigidbody2D body;

    [Header("Groundcheck")]
    protected BoxCollider2D box;
    [System.NonSerialized] public bool isGrounded;
    public virtual void Awake()
    {
        GroundLayer = LayerMask.GetMask("Ground");
        WallLayer = LayerMask.GetMask("Wall");
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
    }

    public virtual void CheckGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
        if (raycastHit.collider != null) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }  

    public virtual bool OnWall(int originalDirection = 1)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, new Vector2( originalDirection * transform.localScale.x, 0), 0.1f, WallLayer);
        return raycastHit.collider != null; 
    }

}
