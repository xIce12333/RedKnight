using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : Spawner
{

    public override void StartSpawn()
    {
        StartCoroutine(SpawningCoroutine());
    }

    public override IEnumerator SpawningCoroutine()
    {
        // ボスが死んだらHP回復薬のドロップを止めます。
        while(!boss.GetComponent<EnemyHealth>().isDead) {
            spawnItem();
            yield return new WaitForSeconds(spawnDelay);
        }

    }
}
