using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int roundNumber = 1;
    private int finalRoundNumber = 5;
    private int enemyRoundMax = 25;
    private int enemiesSpawned = 0;

    public event Action<int, int> OnCountUpdate;
    public event Action<int> OnNewRound;
    public event Action OnFinalRoundComplete;


    [Header("Other Managers")]
    [SerializeField] EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner.OnSpawn += UpdateLiveCount;
    }

    void UpdateLiveCount(GameObject enemy)
    {
        enemiesSpawned++;
        OnCountUpdate?.Invoke(enemyRoundMax - enemiesSpawned, enemyRoundMax );
        if(enemiesSpawned >= enemyRoundMax)
        {
            roundNumber++;
            enemyRoundMax *= 2;
            enemiesSpawned = 0;
            OnNewRound?.Invoke(roundNumber);
        }
        if (roundNumber > finalRoundNumber)
        {
            OnFinalRoundComplete?.Invoke();
        }
    }
}
