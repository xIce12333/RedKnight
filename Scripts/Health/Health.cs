using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
    private float currentHealth;
    [System.NonSerialized] public bool inVulnerable;        // True ==> can't take damage, vice versa

    [Header("iFrames")]
    [SerializeField] protected float iFramesDuration = 0.2f;
    public int numberOfFlashes = 1;
    public virtual void Awake()
    {
        currentHealth = StartingHealth;
    }

    public virtual void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingHealth);
    }
    public void AddHealth(float _value)
    {
        SoundManager.instance.PlaySound("HP Recover");
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, StartingHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetStartingHealth()
    {
        return StartingHealth;
    }
    public void SetHealth(float _health)
    {
        currentHealth = _health;
    }
}
