using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnpoint;


    // "I just spawned something."
    public event Action<GameObject> OnSpawn;

    // Spawn timers
    private float spawnInterval;
    private float nextSpawnTime;

    private bool isGameOver = false;

    [Header("Managers")]
    [SerializeField] RoundManager roundManager;


    void Start()
    {
        // Set first spawn time.
        spawnInterval = 1.2f;
        roundManager.OnFinalRoundComplete += StopSpawning;
        roundManager.OnNewRound += UpdateRoundSpeed;
        SetNextSpawnTime();
    }

    void FixedUpdate()
    {
        TrySpawn();
    }

    private void TrySpawn()
    {
        // If ready to spawn
        if (Time.time >= nextSpawnTime && !isGameOver)
        {
            // Spawn enemy, unlock movement, reset timer
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawnpoint.position, Quaternion.identity);

            OnSpawn?.Invoke(newEnemy);

            newEnemy.GetComponent<Rigidbody>().isKinematic = false;
            SetNextSpawnTime();
        }
    }

    // Time until next spawn function
    void SetNextSpawnTime() => nextSpawnTime = Time.time + spawnInterval;


    void UpdateRoundSpeed(int round)
    {
        spawnInterval -= .2f;
        spawnInterval = Mathf.Max(spawnInterval, 0.25f);
    }

    void StopSpawning()
    {
        isGameOver = true;
    }
}
