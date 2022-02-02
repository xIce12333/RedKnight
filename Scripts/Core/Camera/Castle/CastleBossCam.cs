using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBossCam : BossCamBase
{

    [SerializeField] private BossDoor closingDoor;
    [SerializeField] private BoxCollider2D saveRoomCollider;
    private CastleCamera cam;
    private void Awake()
    {
        cam = FindObjectOfType<CastleCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !inBossRoom) {
            cam.EnterBossRoom(BossRoom);
            closingDoor.CloseTheDoor();
            inBossRoom = true;
            saveRoomCollider.enabled = false;
        }
    } 
}
