using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    [Header("Moving Distance")]
    [SerializeField] private float distance;
    private bool atStartPos = true;
    private bool atEndPos;
    private Vector2 velocity = Vector3.zero;
    private float smoothTime = 0.8f;
    private float ran;
    private float timer;
    private void Awake()
    {
        startPos = transform.position;
        endPos = new Vector2(transform.position.x, transform.position.y + distance);
        ran = Random.Range(0f, 3f);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        CheckPos();
        if (timer >= ran)
            MovePos();
    }  


    private void CheckPos()
    {
        if (Mathf.Abs(startPos.y - transform.position.y) < 0.1) {
            atStartPos = true;
            atEndPos = false;
        }
        else if (Mathf.Abs(endPos.y - transform.position.y) < 0.1) {
            atStartPos = false;
            atEndPos = true;
        }
    }

    private void MovePos()
    {
        if (atStartPos) 
            transform.position = Vector2.SmoothDamp(transform.position, new Vector2(endPos.x, endPos.y), ref velocity, smoothTime);   
        else if (atEndPos)
            transform.position = Vector2.SmoothDamp(transform.position, new Vector2(startPos.x, startPos.y), ref velocity, smoothTime);  
    }
}
