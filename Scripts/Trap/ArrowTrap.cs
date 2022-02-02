using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    // object pooling
    [Header("Attack CoolDown")]
    [SerializeField] private float attackCooldown;

    [Header("Arrow")]
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;       
    private float cooldownTimer;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
            Attack();  
    }

    private int FindArrow()         // Find arrow that is not active 　空いてる（active状態ではない）arrowを探します
    {
        for (int i = 0; i < arrows.Length; i++) {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Attack()
    {
        if (GetComponent<Renderer>().isVisible)
            SoundManager.instance.PlaySound("ArrowTrap");
        cooldownTimer = 0;
        arrows[FindArrow()].transform.position = arrowPoint.position;
        arrows[FindArrow()].GetComponent<TrapProjectile>().ActivateProjectile();
    }
}
