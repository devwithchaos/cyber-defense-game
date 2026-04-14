using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public static float timeBetweenFire = 4;
    [SerializeField] Transform bulletSpawn;

    public static float radius = 10f;


    private float timeToNextFire;


    Transform currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider collider in hits)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                currentTarget = collider.transform;
                transform.LookAt(currentTarget);

                if (Time.time >= timeToNextFire)
                {
                    Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                    Reload();
                }
                break;
            }
        }
    }

    void Reload()
    {
        timeToNextFire = Time.time + timeBetweenFire;
    }
}
