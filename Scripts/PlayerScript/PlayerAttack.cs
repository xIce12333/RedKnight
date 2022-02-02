using UnityEngine;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement player;
    private PlayerHealth health;
    private float attackCoolDown = 0.3f;
    private float cooldownTimer;
    [System.NonSerialized] public float damage = 1f;    // attack power
    public bool isAttacking  {get; private set;}    // public for player animation

    [Header("Reference")]
    [SerializeField] GameObject attackHitBox;
    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        attackHitBox.SetActive(false);
    }

    private void Update()
    {
        if (CanAttack() && cooldownTimer > attackCoolDown && Input.GetKeyDown(KeyCode.K)) {
            StartCoroutine(Attack());
        }
        cooldownTimer += Time.deltaTime;
    }
    private bool CanAttack()
    {
        return !player.isDashing && !health.inVulnerable;
    }

    IEnumerator Attack()        // プレイヤーが攻撃しているときだけ、攻撃のヒットボックスをオンにします
    {
        SoundManager.instance.PlaySound("Player Slash");
        isAttacking = true;
        cooldownTimer = 0;
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
        attackHitBox.SetActive(false);
    }
}
