using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    // stop the game when player takes damage, which make it easier for player to notice
    // プレイヤーがダメージを喰らったら、一時的に時間を止めて、プレイヤーに反応時間を与えます。
    private float speed;
    private bool restoreTime;

    private void Update()
    {
        if (restoreTime) {
            if (Time.timeScale < 1f)
                Time.timeScale += Time.deltaTime * speed;
            else {
                Time.timeScale = 1f;
                restoreTime = false;
            }
        }
    }

    public void StopTime(float changeTime, int restoreSpeed, float delay)
    {
        speed = restoreSpeed;

        if (delay > 0) {
            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
        }
        else
            restoreTime = true;

        Time.timeScale = changeTime;
    }

    private IEnumerator StartTimeAgain(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        restoreTime = true;
    }

}
