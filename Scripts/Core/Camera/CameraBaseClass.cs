using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBaseClass : MonoBehaviour
{
    protected Transform player;
    protected float cameraSpeed = 0.3f;
    [System.NonSerialized] public bool isFollowing = true;
    [System.NonSerialized] public bool inBossRoom;
    protected float leftxLimit;
    [SerializeField] protected Transform Room1;
    protected Vector3 velocity = Vector3.zero;
    protected float yPos;
    protected Vector2 targetPos;

    public virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
