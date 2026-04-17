    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraRotate : MonoBehaviour
    {
        // Transform to rotate around
        [SerializeField] Transform ground;

        // Input A-D
        private float xInput;

        void Update()
        {
            // Get A-D input
            xInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            // Rotate right
            if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(ground.transform.up, Vector3.up, -xInput * 5f);
            }
            // Rotate left
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(ground.transform.up, Vector3.down, xInput * 5f);
            }
    }
    }
