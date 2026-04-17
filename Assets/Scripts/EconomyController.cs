using System;
using System.Collections.Generic;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    // Coins
    public int Coins;
    [SerializeField] EnemySpawner spawnerScript;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<GameObject> turretSpawners;

    public event Action<EconomyController> OnRequestScript;

    private int coinPerKill;

    // "The player earned coins"
    public event Action<int> OnBalanceUpdate;

    [Header("Other Managers")]
    [SerializeField] UpgradeManager upgradeManager;

    // Constants
    const int STARTING_COIN_AMOUNT = 150;


    void Start()
    {
        // Init to 0
        Coins = STARTING_COIN_AMOUNT;
        spawnerScript.OnSpawn += AddEnemyToList;
        OnRequestScript?.Invoke(this);
        upgradeManager.OnUpgrade += UpgradeLoot;
        coinPerKill = 2;
    }

    void AddEnemyToList(GameObject enemy) { 
        enemies.Add(enemy);
        enemy.GetComponent<EnemyBehavior>().OnDeath += AddCoins;
        enemy.GetComponent<EnemyBehavior>().OnDeath += RemoveEnemyFromList;
    }

    void RemoveEnemyFromList(GameObject enemy)
    {
        enemy.GetComponent<EnemyBehavior>().OnDeath -= AddCoins;
        enemy.GetComponent<EnemyBehavior>().OnDeath -= RemoveEnemyFromList;
        enemies.RemoveAt(enemies.IndexOf(enemy));
    }

    void AddCoins(GameObject enemy)
    {
        Coins += coinPerKill;
        UpdateBalanceUI();
    }

    public bool TryPurchase(int amount)
    {
        if(Coins < amount) return false;
        Coins -= amount;
        UpdateBalanceUI();
        return true;
    }

    void UpdateBalanceUI() => OnBalanceUpdate?.Invoke(Coins);

    void UpgradeLoot(int option) {

        if (option != 2) return;
        coinPerKill++;
    }


    public int GetEnemyCount => enemies.Count;











    //public void UpgradeROF()
    //{
    //    if (TurretBehavior.timeBetweenFire <= .5f) { upgradePriceLabel.text = "MAX!"; return; }
    //    if (Coins < 50) return;
    //    Coins -= 50;
    //    TurretBehavior.timeBetweenFire -= .5f;
    //    Debug.Log(TurretBehavior.timeBetweenFire);
    //}

    //public void UpgradeSpawnRate()
    //{
    //    if (EnemySpawner.spawnInterval <= 0.5f) { upgradePriceLabel_2.text = "MAX!"; return; }
    //    if (Coins < 50) return;
    //    Coins -= 50;
    //    EnemySpawner.spawnInterval -= .15f;

    //}

    //public void UpgradeTurretRadius()
    //{
    //    if (TurretBehavior.radius >= 25f) { upgradePriceLabel_3.text = "MAX!"; return; }
    //    if (Coins < 50) return;
    //    TurretBehavior.radius += 2.5f;
    //    Coins -= 50;
    //}
}
