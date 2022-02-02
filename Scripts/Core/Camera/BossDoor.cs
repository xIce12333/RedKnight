using UnityEngine;
using System.Collections;
public class BossDoor : MonoBehaviour
{
    // For Castle, close the door when player enters boss room
    // 試練の間で、プレイヤーがボス部屋に入ったら、ドアを閉じます
    private float doorSpeed = 10;
    private bool close = false;
    private float timer;

    void Awake()
    {
        timer = 0f;
    }

    void Update()
    {
        if (close) {
            timer += Time.deltaTime;
            if (timer <= 0.2f)
                transform.position = transform.position + new Vector3(0, -doorSpeed*Time.deltaTime, 0);
        }
    } 

    public void CloseTheDoor()
    {
        close = true;
    }
}
