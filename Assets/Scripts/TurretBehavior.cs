using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    // Bullet Prefab + Spawnpoint
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnpoint;


    public static int TurretCount = 0;
    public TurretSpawner mySpawner;

    // Turret stats, range and speed
    private static float radius = 10f;
    private static float timeBetweenFire = 3f;
    private float timeToNextFire;

    // Current Target
    Transform currentTarget;

    [Header("Managers")]
    [SerializeField] UpgradeManager upgradeManager;
    
    void Start()
    {
        // Reset firing timer so turret can't instantly shoot
        Reload();
    }

    private void Awake()
    {
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        upgradeManager.OnUpgrade += UpgradeChoose;

    }

    void Update()
    {
        // All objects in range
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        // If there isn't already a target
        if(currentTarget == null)
        {
            foreach (Collider collider in hits)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    // Lock target
                    currentTarget = collider.transform;
                }
            }
        }

        if (currentTarget != null) { 
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distance > radius) currentTarget = null;
        }


        // If no target in sight, look again
        if (currentTarget == null) return;

        // Aim at target
        Vector3 lookTarget = currentTarget.transform.position;
        lookTarget.y = 500f;
        transform.LookAt(lookTarget);
        // If not reloading
        if (Time.time >= timeToNextFire)
        {
            // Create bullet, set its target, untarget enemy
            GameObject firedBullet = Instantiate(bullet, bulletSpawnpoint.transform.position, Quaternion.identity);
            firedBullet.GetComponent<BulletBehavior>().SetTarget(currentTarget);
            Reload();
        }
    }

    // Time between shots function
    void Reload()
    {
        timeToNextFire = Time.time + timeBetweenFire;
    }

    void UpgradeChoose(int option)
    {
        switch (option)
        {
            case 0:
                UpgradeSpeed();
                break;
            case 1:
                UpgradeRadius();
                break;
        }
    }

    void UpgradeRadius()
    {
        radius += 2.5f / TurretCount;
        Debug.Log($"Radius: {radius}");
    }

    void UpgradeSpeed()
    {
        timeBetweenFire -= .25f / TurretCount;
        Debug.Log(timeBetweenFire);
        return;
    }
}
