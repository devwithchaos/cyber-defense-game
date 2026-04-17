using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Transform> OnClickTurretSpawner;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                switch(hit.collider.gameObject.tag)
                {
                    case "TurretSpawner":
                        OnClickTurretSpawner?.Invoke(hit.collider.gameObject.transform);
                        break;
                    default:
                        Debug.Log("Clicked something else!");
                        break;
                }
            }
        }
    }
}
