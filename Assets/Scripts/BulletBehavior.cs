using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Despawn Stats
    private float timeOfDeath;
    private float timeToDespawn = 5f;
    // What's being shot?
    private Transform currentTarget;
    // How fast?
    private float bulletSpeed;


    void Start()
    {
        // Despawn if not hit timer
        timeOfDeath = Time.time + timeToDespawn;
        bulletSpeed = 100f;
    }

    void Update()
    {
        // If there is no target
        if (currentTarget == null)
        {
            // Destroy self
            Destroy(gameObject);
            return;
        }

        // If lifetime has expired
        if (Time.time >= timeOfDeath) {
            // Destroy self
            Destroy(gameObject);
            return;
        }

        // Move toward target
        Fire();
    }


    private void Fire()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }


    // When hit something
    private void OnCollisionEnter(Collision collision)
    {
        // If it's an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy enemy
            collision.gameObject.GetComponent<EnemyBehavior>().Die();
            // Destroy self
            Destroy(gameObject);
        }
    }

    // Target setter, called by TurretBehavior.cs
    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }
}
