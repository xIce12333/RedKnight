using System.Collections;
using UnityEngine;

public class SlimeForestCamera : CameraBaseClass
{
    private float lowyLimit;
    private Camera mainCamera;
    public override void Awake()
    {
        base.Awake();
        leftxLimit = Room1.position.x - 11f;
        lowyLimit = Room1.position.y - 4.5f;
        mainCamera = UnityEngine.Camera.main;
    }
    private void Update()
    {
        if (isFollowing)                //  カメラがマップ外を映さないために、Clampを使います
            transform.position = new Vector3(Mathf.Clamp(player.position.x, leftxLimit, float.MaxValue), Mathf.Clamp(player.position.y, lowyLimit, float.MaxValue), transform.position.z);
        else if (inBossRoom)        //　スムーズにカメラの位置を調整するために、SmoothDampを使います
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPos.x, targetPos.y, transform.position.z), ref velocity, cameraSpeed);
    }

    public void EnterBossRoom(Transform _bossRoom)
    {
        isFollowing = false;
        inBossRoom = true;
        targetPos.x = _bossRoom.position.x;
        targetPos.y = _bossRoom.position.y - 2.5f;
        StartCoroutine(cameraDimension());
    }

    private IEnumerator cameraDimension()
    {
        while (mainCamera.orthographicSize < 6.5f) {
            mainCamera.orthographicSize += 0.01f;
            yield return null;
        }
    }
}
