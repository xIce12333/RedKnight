using UnityEngine;

public class BossCamBase : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] protected Transform BossRoom;
    protected bool inBossRoom = false;
    private void Start()        // Use start because start is called after Awake, prevent null reference
    {
        FindObjectOfType<SoundManager>().PlaySound("BGM");
    }
}
