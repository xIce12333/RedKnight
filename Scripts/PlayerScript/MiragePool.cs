using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiragePool : MonoBehaviour
{
    [SerializeField] private GameObject[] mirages;
    private PlayerMovement playerMovement;
    private float distanceBetweenMirage = 0.05f;
    
    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.isDashing) {
            // プレイヤーの位置と残像の位置が "distanceBetweenMirage"という距離を過ぎたら、次の残像をオンにします
            if (Mathf.Abs(transform.position.x - playerMovement.lastMirageXpos) >= distanceBetweenMirage) {
                mirages[FindMirage()].SetActive(true);
                playerMovement.lastMirageXpos = transform.position.x;
            }
        }
    }


    
    private int FindMirage()            // find mirage object that is not active        空いてる（activeではない）残像を探します
    {
        for (int i = 0; i < mirages.Length; i++) {
            if (!mirages[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
