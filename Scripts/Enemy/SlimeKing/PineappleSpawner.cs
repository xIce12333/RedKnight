using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleSpawner : Spawner
{
    //　true　=>　ボスが左側にいます    false　=>　ボスが右側にいます
    private bool bossAtLeftSide;     // true if boss is at left side of the room, otherwise false
    private SlimeKing slimeKing;

    public override void Awake()
    {
        base.Awake();
        slimeKing = boss.GetComponent<SlimeKing>();
        bossAtLeftSide = (slimeKing.transform.position.x <= (rightLimit.position.x + leftLimit.position.x) / 2);
    }

    public override float FindRandomPos()
    {
        if (slimeKing.stage == 1)       // stage 1 random position  ボス（スライムキング）が１段階目の場合、ランダムでトラップ落とす場所を選びます。
            return base.FindRandomPos();

        // for boss stage 2 and 3       
        //　ボスが２、３段階目の場合、ボスがいる側にトラップを落とす確率を高めて、プレイヤーが簡単にボスに近づけられないようにします。
        int separation = Random.Range(0, spawnSeparationNum / 2);    // for object position
        int rand = Random.Range(0, slimeKing.stage + 1);                        // spawning left or right, value of 0 to 2 for stage 2, 0 to 3 for stage 3
        if (bossAtLeftSide)     
            // boss at left side, 2/3 (stage 2) and 3/4 (stage 3) probability to spawn at left side
            //　ボスが左側にいるので、2/3（２段階目）もしくは　3/4（３段階目）　の確率で左側にトラップを落とします。
            return (rand > 0) ? leftLimit.position.x + spawnSeparation * separation : rightLimit.position.x - spawnSeparation * separation;
        // boss at right side, 2/3 (stage 2) and 3/4 (stage 3) probabiliy to spawn at right side
        //　ボスが右側にいるので、2/3（２段階目）もしくは　3/4（３段階目）　の確率で左側にトラップを落とします。
        return (rand > 0) ? rightLimit.position.x - spawnSeparation * separation : leftLimit.position.x + spawnSeparation * separation;
    }
}
