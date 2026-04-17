using System;
using UnityEngine;

public class PlayerCubeController : MonoBehaviour
{
    public event Action OnHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(45, -45, 45) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) { OnHit?.Invoke(); }
    }
}
