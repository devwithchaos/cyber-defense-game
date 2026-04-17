using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Path transforms + RB
    [SerializeField] Transform[] pathPoints;
    [SerializeField] Transform nextPoint;
    [SerializeField] Rigidbody rb;

    // "I just died"
    public event Action<GameObject> OnDeath;

    // Private use speed and path index
    private float speed;
    private int pathPointIndex = 0;

    // Constants
    const float PATH_FINDING_TOLERANCE = 1.5f;


    void Start()
    {
        // Init speed, assign next point
        speed = 75f;
        NextPoint();
    }

    void Update()
    {
        // Lookat level equalization
        Vector3 levelPoint = nextPoint.position;
        levelPoint.y = 1.5f;


        // Look at the next point
        transform.LookAt(levelPoint);
        // Go toward it
        rb.AddForce(transform.forward * speed * Time.deltaTime);


        // How close to next point
        Vector3 distanceToPoint = transform.position - levelPoint;

        // If at or close to point, focus on the next one.
        if (Mathf.Sqrt(Mathf.Pow(distanceToPoint.x, 2) + Mathf.Pow(distanceToPoint.z,2)) < PATH_FINDING_TOLERANCE)
        {
            NextPoint();
        }
    }

    // Get transform from next path point
    void NextPoint() { nextPoint = pathPoints[pathPointIndex]; pathPointIndex++; }


    // If hit something
    private void OnCollisionEnter(Collision collision)
    {
        // If hit the player base/cube, destroy self.
        if (collision.gameObject.CompareTag("PlayerCube")) Die();
    }

    public void Die()
    {
        OnDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
