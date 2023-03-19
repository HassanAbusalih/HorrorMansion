using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateResizingGun : MonoBehaviour
{
    [SerializeField] KeyCode enlargeButton = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkButton = KeyCode.Mouse1;
    [SerializeField] FloatList resizeSpeed;
    AlternateResizable resizable;
    Transform rayDirection;

    private void Start()
    {
        rayDirection = FindObjectOfType<FirstPersonCam>().transform;
    }

    void Update()
    {
        if (Input.GetKey(enlargeButton))
        {
            CheckForResizable();
            if (resizable != null)
            {
                resizable.Enlarge(resizeSpeed.GetFloatVar("Resize Speed").value);
            }
        }
        else if (Input.GetKey(shrinkButton))
        {
            CheckForResizable();
            if (resizable != null)
            {
                resizable.Shrink(resizeSpeed.GetFloatVar("Resize Speed").value);
            }
        }
    }

    void CheckForResizable()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayDirection.position, rayDirection.forward, out hit, 5f))
        {
            if (hit.transform.GetComponent<AlternateResizable>() != null)
            {
                resizable = hit.transform.GetComponent<AlternateResizable>();
            }
            else
            {
                resizable = null;
            }
        }
        else
        {
            resizable = null;
        }
    }
}
