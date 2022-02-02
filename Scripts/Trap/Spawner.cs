using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] protected Transform leftLimit;
    [SerializeField] protected Transform rightLimit;
    [SerializeField] protected GameObject boss;
    protected float spawnSeparation;
    
    [Header("Spawn Paremeters")]
    [SerializeField] protected float spawnDelay = 1f;
    [SerializeField] protected float duration = 5f;
    [SerializeField] protected int spawnSeparationNum = 10;
    private float timer;

    public virtual void Awake()
    {
        spawnSeparation = Mathf.Abs(rightLimit.position.x - leftLimit.position.x) / spawnSeparationNum;
        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    public virtual void spawnItem()
    {
        GameObject obj = Instantiate(objectPrefab) as GameObject;
        obj.transform.position = new Vector2(FindRandomPos(), leftLimit.position.y);
    }

    public virtual float FindRandomPos()
    {
        int ran = Random.Range(0, spawnSeparationNum);
        return leftLimit.position.x + spawnSeparation * ran;
    }

    public void StartSpawn(float _spawnDelay, float _duration, int _spawnSeparationNum)
    {
        spawnDelay = _spawnDelay;
        duration = _duration;
        spawnSeparationNum = _spawnSeparationNum;
        StopAllCoroutines();
        timer = 0f;
        StartCoroutine(SpawningCoroutine());
    }

    public virtual void StartSpawn()        // virtual for healthcollectible spawner
    {
        StopAllCoroutines();
        timer = 0f;
        StartCoroutine(SpawningCoroutine());
    }

    public virtual IEnumerator SpawningCoroutine()      // virtual for healthcollectible spawner 
    {
        while (timer <= duration && !boss.GetComponent<EnemyHealth>().isDead) {
            spawnItem();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
