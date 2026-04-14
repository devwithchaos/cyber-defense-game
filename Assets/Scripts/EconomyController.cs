using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyController : MonoBehaviour
{

    public static int Coins;
    [SerializeField] TMPro.TextMeshProUGUI upgradePriceLabel;
    [SerializeField] TMPro.TextMeshProUGUI upgradePriceLabel_2;
    [SerializeField] TMPro.TextMeshProUGUI upgradePriceLabel_3;

    void Start()
    {
        Coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeROF()
    {
        if (TurretBehavior.timeBetweenFire <= .5f) { upgradePriceLabel.text = "MAX!"; return; }
        if (Coins < 50) return;
        Coins -= 50;
        TurretBehavior.timeBetweenFire -= .5f;
        Debug.Log(TurretBehavior.timeBetweenFire);
    }

    public void UpgradeSpawnRate()
    {
        if (EnemySpawner.spawnInterval <= 0.5f) { upgradePriceLabel_2.text = "MAX!"; return; }
        if (Coins < 50) return;
        Coins -= 50;
        EnemySpawner.spawnInterval -= .15f;

    }

    public void UpgradeTurretRadius()
    {
        if (TurretBehavior.radius >= 25f) { upgradePriceLabel_3.text = "MAX!"; return; }
        if (Coins < 50) return;
        TurretBehavior.radius += 2.5f;
        Coins -= 50;
    }
}
