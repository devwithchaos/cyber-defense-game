using System;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    // Turret Prefab
    [SerializeField] GameObject turretPrefab;
    [SerializeField] Transform[] turretSpawnPoints;

    [Header("Other Managers")]
    [SerializeField] InputManager inputManager;
    [SerializeField] EconomyController economyController;


    // Constants
    const int TURRET_COST = 50;

    void Start()
    {
        inputManager.OnClickTurretSpawner += SpawnTurret;
    }

    void SpawnTurret(Transform turretTransform)
    {
        // If can't afford, return
        if (!economyController.TryPurchase(TURRET_COST)) return;
        GameObject newTurret = Instantiate(turretPrefab, turretTransform.position, Quaternion.identity);
        newTurret.transform.LookAt(new Vector3(0, 90, 0));
        turretTransform.gameObject.tag = "Turret";
        newTurret.tag = "Turret";
        TurretBehavior.TurretCount++;
    }
}
