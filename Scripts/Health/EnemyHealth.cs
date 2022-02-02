using UnityEngine;
using System.Collections;

public class EnemyHealth : Health
{
    public bool isDead {get; private set;}
    private SpriteRenderer render; 
    [Header("Is the enemy a boss?")]
    [SerializeField] private bool isBoss;

    [Header("Flash Material")]
    [SerializeField] protected Material flashMaterial;
    protected Material originalMaterial;

    [Header("Coin")]
    [SerializeField] private GameObject Coin;
    public override void Awake()
    {
        base.Awake();
        if (GetComponent<SpriteRenderer>() != null)     {           // some enemy don't have single SpriteRenderer, e.g. GrayKnight
            render = GetComponent<SpriteRenderer>();
            originalMaterial = render.material;
        }    
            
        
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
        if (GetCurrentHealth() > 0) {
            StartCoroutine(Invulnerability());       
        }
        else {
            if (!isDead) {
                isDead = true;
                SoundManager.instance.PlaySound("Enemy Hurt");
                if (!isBoss) {
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponentInChildren<BoxCollider2D>().enabled = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    SpawnCoin();
                }
                
            }
        }
    }

    private void SpawnCoin()
    {
        Coin.SetActive(true);
        Coin.transform.position = this.transform.position;
        Coin.transform.parent = null;           // prevent the coin from sticking with the enemy　敵が死んで、出たコインが敵の速度に影響されないようにします
        Coin.GetComponent<RigidCoin>().ThrowUp();
        SoundManager.instance.PlaySound("Rigid Coin");
    }

    public virtual IEnumerator Invulnerability() 
    {
        inVulnerable = true;
        for(int i=0; i<numberOfFlashes; i++) {
            render.material = flashMaterial;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            render.material = originalMaterial;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        inVulnerable = false;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerAttack" && !inVulnerable) {
            this.TakeDamage(FindObjectOfType<PlayerAttack>().damage);
            Debug.Log(GetCurrentHealth());
        }
    }
}
