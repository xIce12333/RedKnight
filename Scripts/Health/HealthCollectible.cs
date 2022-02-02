using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [Header("Health Value")]
    [SerializeField] private float healthValue = 1f;
    public virtual void OnTriggerEnter2D(Collider2D collision)      // virtual for healthCollectileDropping (boss room HP recovery item)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerHealth>().AddHealth(healthValue);
            Destroy(this.gameObject);
        }
    }
}
