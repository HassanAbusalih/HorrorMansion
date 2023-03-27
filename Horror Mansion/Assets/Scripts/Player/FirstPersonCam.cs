using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    float xRotation;
    float yRotation;
    [SerializeField] FloatList floatList;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * floatList.GetFloatVar("Sensitivity").value;
        yRotation += Input.GetAxis("Mouse X") * floatList.GetFloatVar("Sensitivity").value;
        xRotation = Mathf.Clamp(xRotation, -85, 85);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        if (transform.parent != null)
        {
            transform.parent.forward = Vector3.ProjectOnPlane(transform.forward, new Vector3(0, 1));
        }
    }
}
