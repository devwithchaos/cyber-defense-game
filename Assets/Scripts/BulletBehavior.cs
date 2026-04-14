using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public static float bulletSpeed;
    [SerializeField] Transform currentTarget;
    [SerializeField] float timeToDespawn = 3f;
    [SerializeField] Rigidbody rb;

    private float timeOfDeath;
    private bool hasTarget = false;


    // Start is called before the first frame update
    void Start()
    {
        timeOfDeath = Time.time + timeToDespawn;
        rb = GetComponent<Rigidbody>();
        bulletSpeed = 100f;
    }

    // Update is called once per frame
    void Update()
    {

        if(transform == null) Destroy(gameObject);
        float radius = 15f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);




        foreach (Collider collider in hits)
        {
            if (hasTarget) break;
            if (collider.gameObject.CompareTag("Enemy"))
            {
                currentTarget = collider.transform;
                hasTarget = true;
                break;
            }
        }

        if(Time.time >= timeOfDeath) { Destroy(gameObject); }


        transform.LookAt(currentTarget);
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * bulletSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EconomyController.Coins+=2;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
