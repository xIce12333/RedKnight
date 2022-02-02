using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // For Slime Forest boss attack    スライムの森のボスが特定のアタックをしたとき、カメラを振動させます
    [SerializeField] private Transform Room;
    public IEnumerator ShakeHorizontal(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float timer = 0f;

        while (timer < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(Room.position.x + x, transform.position.y, transform.position.z);
            SoundManager.instance.PlaySoundIfNotPlaying("Earthquake");
            timer += Time.deltaTime;
            yield return null;
        }
        SoundManager.instance.StopSound("Earthquake");
        transform.position = originalPosition;
    }

    public IEnumerator ShakeVertical(float duration, float magnitude)
    {
        Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        float timer = 0f;

        while (timer < duration) {
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(transform.position.x, originalPosition.y + y, transform.position.z);

            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }        
    
}
