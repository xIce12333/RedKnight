using UnityEngine;

public class CastleCamera : CameraBaseClass
{
    private float maxValue;
    [System.NonSerialized] public bool isFalling;
    public override void Awake()
    {
        base.Awake();
        leftxLimit = Room1.position.x - 1.75f;
        yPos = Room1.position.y;
    }

    void Update()
    {
        if (isFollowing)        
            transform.position = new Vector3(Mathf.Clamp(player.position.x, leftxLimit, float.MaxValue), yPos, transform.position.z);
        else if (isFalling) {   // falling trap  落下中のカメラワーク
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPos.x, transform.position.y, transform.position.z), ref velocity, cameraSpeed);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.transform.position.y - targetPos.y, float.MinValue, Room1.position.y), transform.position.z);
        }
        else if (inBossRoom)    
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPos.x, transform.position.y, transform.position.z), ref velocity, cameraSpeed);
        

    }
    public void EnterBossRoom(Transform _bossRoom)
    {
        isFollowing = false;
        inBossRoom = true;
        targetPos.x = _bossRoom.position.x - 3.2f;
    }

    public void Falling(Transform xPos, float yPos)
    {
        isFalling = true;
        isFollowing = false;
        targetPos.x = xPos.position.x;
        targetPos.y = yPos;
    }

    public void EndFalling(Transform RoomPos)
    {
        isFalling = false;
        leftxLimit = RoomPos.position.x + 0.45f;
        yPos = RoomPos.position.y;
        isFollowing = true;
    }
}   
