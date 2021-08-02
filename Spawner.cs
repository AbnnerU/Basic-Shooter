using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;


    private void Start()
    {
        PoolManager.SpawnObject(prefabs, new Vector2(0, 4), Quaternion.identity);
        PoolManager.SpawnObject(prefabs, new Vector2(3, 4), Quaternion.identity);
    }

    public void Respawn(GameObject obj, Vector2 position, Quaternion rotation, float releaseTime, float spawnTime)
    {
        if (releaseTime >= spawnTime)
        {
            releaseTime = spawnTime;
            releaseTime++;
        }
       

        StartCoroutine(ReleaseObj(obj,releaseTime));

        StartCoroutine(SpawnObj(obj, position, rotation, spawnTime));
           
    }

    IEnumerator SpawnObj(GameObject obj, Vector2 position, Quaternion rotation,float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.SpawnObject(prefabs, position, rotation);
        StopCoroutine(SpawnObj(obj, position, rotation, time));
    }


    IEnumerator ReleaseObj (GameObject obj, float time)
    {
        print(obj);
        yield return new WaitForSeconds(time);
        PoolManager.ReleaseObject(obj);
        StopCoroutine(ReleaseObj(obj, time));

    }
}
