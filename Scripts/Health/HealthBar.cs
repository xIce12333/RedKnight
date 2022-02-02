using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Awake()
    {
        totalHealthBar.fillAmount = 0.5f;       // 5 HP  (1f = 10HP) 
    }


    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.GetCurrentHealth() / 10;
    }
}
