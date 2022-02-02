using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraXpos;
    private float cameraYPos;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            if (collision.transform.position.x < transform.position.x) {
                FindObjectOfType<CastleCamera>().isFollowing = true;
                FindObjectOfType<CastleCamera>().isFalling = false;
            }
            else {
                cameraYPos = Mathf.Abs(FindObjectOfType<CastleCamera>().transform.position.y - FindObjectOfType<PlayerMovement>().transform.position.y);
                FindObjectOfType<CastleCamera>().Falling(cameraXpos, cameraYPos);
            }
        }
    }
}
