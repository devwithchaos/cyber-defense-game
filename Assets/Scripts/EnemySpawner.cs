using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static float spawnInterval;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnPoint;

    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        SetNextSpawnTime();
        spawnInterval = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time >= nextSpawnTime)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            newEnemy.GetComponent<Rigidbody>().isKinematic = false; 
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime() => nextSpawnTime = Time.time + spawnInterval;
}
