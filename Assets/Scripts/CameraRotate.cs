    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] Transform ground;
        private float xInput;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            xInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(ground.transform.up, Vector3.up, -xInput * 5f);
                Debug.Log("Hello?");
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(ground.transform.up, Vector3.down, xInput * 5f);
                Debug.Log("Hello?");
            }
    }
    }
