using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  dash mirage, object pooling
public class Mirage : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer playerRender;
    [SerializeField] private Transform mirageParent;

    private float activeTime = 0.2f;
    private float timeActivated;
    private float alpha;
    private float alphaMax = 0.8f;
    private float alphaMultiplier = 0.95f;


    private void OnEnable()
    {
        transform.parent = null;            // prevent mirage following player to move　残像がプレイヤーのいる場所に影響されないようにします
        alpha = alphaMax;
        GetComponent<SpriteRenderer>().sprite = playerRender.sprite; 
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;
        timeActivated = 0f;
    }

    private void Update()
    {
        // 徐々に残像をより透明にします
        alpha *= alphaMultiplier;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        if (timeActivated >= activeTime) {
            transform.parent = mirageParent;        // set the parent back　
            gameObject.SetActive(false);
        }
        else
            timeActivated += Time.deltaTime;
    }
}
