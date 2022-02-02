using UnityEngine;

public class EnemyDamage : Character
{
    [Header("Enemy Damage")]
    [System.NonSerialized] public float damage = 1f;        //　敵がプレイヤーに与えるダメージ

    public override void Awake()
    {
        base.Awake();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !GetComponent<EnemyHealth>().isDead) {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            collision.GetComponent<TimeStop>().StopTime(0.05f, 20, 0.2f);
        }
    }

}
