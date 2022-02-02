using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    protected float damage = 1f;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            collision.GetComponent<TimeStop>().StopTime(0.05f, 20, 0.2f);
        }
    }
}
