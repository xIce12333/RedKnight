using UnityEngine;

public class pineapple : TrapDamage
{
    [SerializeField] private float speed = 10f;             // falling speed

    private bool soundPlayed;       // ensure only play once

    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -speed);
    }

    private void Update()
    {
        if (GetComponent<Renderer>().isVisible && !soundPlayed)  {          // seen in camera
            soundPlayed = true;
            SoundManager.instance.PlaySound("Object Falling");
        }   
            
    }

    public override void OnTriggerEnter2D(Collider2D collision)         // deal damage and destroying object
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player" || collision.tag == "Ground") {
            SoundManager.instance.PlaySound("Small Explosion");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetTrigger("explosion");
        }
            
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
