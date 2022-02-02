using UnityEngine;

public class SlimeMovement : Enemy
{
    
    public override void Awake()
    {
        base.Awake();
        EnemyScale = 0.8f;
    }
    
    public virtual void Update()        // virtual for slimeSpawn (slime in SlimeForest boss room)
    {
        if (GetComponent<EnemyHealth>().isDead) {
            animator.SetTrigger("die");
            Destroy(gameObject, 0.25f);
        }
        
    }

    public void FacePlayer()
    {
        transform.localScale = new Vector2(-FindPlayerDirection() * EnemyScale, EnemyScale);
    }

    public override bool OnWall(int originalDirection = 1)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, new Vector2( originalDirection * transform.localScale.x, 0), 0.1f, WallLayer);
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, new Vector2( originalDirection * transform.localScale.x, 0), 0.1f, GroundLayer);
        return raycastHit || raycastHit2;
    }

    public bool nearEdge()
    {
        RaycastHit2D raycastHit = Physics2D.Linecast(castPoint.transform.position, new Vector2(castPoint.transform.position.x, castPoint.transform.position.y - 1f), GroundLayer);
        return raycastHit.collider == null;
    }


    //----------SFX----------//

    public void PlayWalkSound()
    {
        if (GetComponent<Renderer>().isVisible)         // in camera
            SoundManager.instance.PlaySound("Slime Walk");
    }

    public void PlayRushSound()
    {
        if(GetComponent<Renderer>().isVisible)          // in camera
            SoundManager.instance.PlaySound("Slime Rush");
    }
}
