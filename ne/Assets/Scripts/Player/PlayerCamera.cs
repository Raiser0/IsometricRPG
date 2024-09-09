using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity;

    private Vector3 input;
    private float mouseInput;

    private void Update()
    {
        input.z = Input.GetAxis("Vertical");
        input.x = Input.GetAxis("Horizontal");

        if (Input.GetMouseButton(1)) 
        {
            mouseInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseInput * sensitivity);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}