using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : Spawner
{
    private int lastRan = -1;    // store last spawned position, prevent overlapping two slime

    public override float FindRandomPos()
    {
        // ボス（スライムキング）がスライムを召喚するとき、前に召喚されたスライムと落下位置が重ならないようにします。
        int ran;
        do {
            ran = Random.Range(0, spawnSeparationNum);
            
        } while (ran == lastRan);
        lastRan = ran;
        return leftLimit.position.x + spawnSeparation * ran;
    }

}
