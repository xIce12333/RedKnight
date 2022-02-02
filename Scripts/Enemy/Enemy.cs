using UnityEngine;

public class Enemy : EnemyDamage
{
    protected Animator animator;
    [System.NonSerialized] public Transform player;
    protected Transform castPoint;

    [Header("AgroRange")]
    [SerializeField] public float agroRange;    //　敵の視界、プレイヤーが視界に入ったら攻撃します

    [Header("Enemy Scale")]
    [System.NonSerialized] public float EnemyScale = 1f;

   public override void Awake()
   {
       base.Awake();
       animator = GetComponent<Animator>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
       castPoint = gameObject.transform.GetChild(0);
   }

   public bool CanSeePlayer(float distance, int direction = 1)
   {
       float castDist = (transform.localScale.x > 0)? -distance : distance;
       Vector2 endPos = castPoint.position + Vector3.right * castDist;
       RaycastHit2D raycastHit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
       return (raycastHit.collider != null && raycastHit.collider.tag == "Player");
   }

   
   public int FindPlayerDirection()
    {   
        return (player.position.x < transform.position.x)? -1 : 1;
    }

}
