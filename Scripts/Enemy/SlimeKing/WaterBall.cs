using UnityEngine;

public class WaterBall : TrapDamage
{
    #region Variables 変数
    [SerializeField] private Transform shootPoint;          // for initial position 
    private Rigidbody2D body;
    private BoxCollider2D box;
    private int direction;              // attack direction
    private float launchSpeed;               // initial speed
    private bool isHit;
    private bool isHorizontal;              // ball type, horizontal or projectile

    #endregion

    #region Functions 関数

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isHorizontal) {            // Projectile
            if (!isHit) {
                if (direction < 0f)
                    ChangeRotation(180f - Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg);
                else
                    ChangeRotation(Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg);
            }
        }
        else if (!isHit){               // Horizontal
            body.velocity = new Vector2(launchSpeed * direction, 0);
        } 
        
    } 



    public void StartAttack(bool _isHorizontal, float _angle, float _direction, float _speed)
    {
        isHit = false;
        launchSpeed = _speed;
        transform.position = shootPoint.position;
        direction = -(int)Mathf.Sign(_direction);
        if (_isHorizontal)
            HorizontalAttack();
        else
            ProjectileAttack(_angle);
        gameObject.SetActive(true);
        body.AddForce(transform.right * launchSpeed * direction, ForceMode2D.Impulse);
        box.enabled = true;
    }

    private void HorizontalAttack()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        isHorizontal = true;
        transform.rotation = Quaternion.identity;
    }

    private void ProjectileAttack(float _angle)
    {
        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        isHorizontal = false;
        ChangeRotation(_angle);
    }



     private void ChangeRotation(float angle)   // 水玉の角度を調整します
     {
        if (direction < 0f)
            transform.rotation = Quaternion.AngleAxis(360f - angle, Vector3.forward);
        else
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
     }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player" || collision.tag == "Ground") {
            isHit = true;     
            body.velocity = Vector2.zero;
            box.enabled = false;
            GetComponent<Animator>().SetTrigger("hit");
        }
    }

    private void DeActivate()       // for animator event to deactivate object
    {
        gameObject.SetActive(false);
    }

    #endregion
}
