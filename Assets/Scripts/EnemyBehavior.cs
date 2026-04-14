using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] Transform[] pathPoints;
    [SerializeField] float speed;
    [SerializeField] Transform nextPoint;
    [SerializeField] Rigidbody rb;

    private int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        NextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 levelPoint = nextPoint.position;
        levelPoint.y = 1.5f;



        transform.LookAt(levelPoint);
        rb.AddForce(transform.forward * speed * Time.deltaTime);


        // How close to point
        Vector3 distanceToPoint = transform.position - levelPoint;

        if (Mathf.Sqrt(Mathf.Pow(distanceToPoint.x, 2) + Mathf.Pow(distanceToPoint.z,2)) < 1.5f)
        {
            NextPoint();
        }
    }

    void NextPoint() { nextPoint = pathPoints[currentIndex]; currentIndex++; }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCube")) Destroy(gameObject);
    }
}
