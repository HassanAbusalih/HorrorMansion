using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to the camera.
/// This script allows the player to rotate the camera using mouse input.
/// The player is rotated along with the camera along the Y axis.
/// </summary>
public class FirstPersonCam : MonoBehaviour
{
    float xRotation;
    float yRotation;
    float sensitivity;
    [SerializeField] FloatList floatList;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sensitivity = floatList.GetFloatVar("Sensitivity").value;
    }

    void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -85, 85);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        if (transform.parent != null)
        {
            transform.parent.forward = Vector3.ProjectOnPlane(transform.forward, new Vector3(0, 1));
        }
    }
}
