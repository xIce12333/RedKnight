using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFallingCamera : MonoBehaviour
{
    [SerializeField] private BoxCollider2D invisibleCeiling;

    private void Awake()
    {
        invisibleCeiling.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            FindObjectOfType<CastleCamera>().EndFalling(transform);
            invisibleCeiling.enabled = true;
        }
            
    }
}
