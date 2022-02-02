using UnityEngine;

public class SlimeForestBossCam : BossCamBase
{
    private SlimeForestCamera cam;
    [SerializeField] private BoxCollider2D Wall;
    private void Awake()
    {
        cam = FindObjectOfType<SlimeForestCamera>();
        Wall.enabled = false;       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            cam.EnterBossRoom(BossRoom);
            inBossRoom = true;
            Wall.enabled = true;        // enable invisible wall when player enters boss room  プレイヤーがボス部屋に入ったら、見えない壁を起動します
            GetComponent<BoxCollider2D>().enabled = false;
        }
    } 
}
