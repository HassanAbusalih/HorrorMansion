using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizingGun : MonoBehaviour
{
    ResizableObject resizable;
    Transform rayDirection;
    [SerializeField] KeyCode enlargeButton = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkButton = KeyCode.Mouse1;

    private void Start()
    {
        rayDirection = FindObjectOfType<FirstPersonCam>().transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(enlargeButton))
        {
            CheckForResizable();
            if (resizable != null)
            {
                resizable.Enlarge();
            }
        }
        else if (Input.GetKeyDown(shrinkButton))
        {
            CheckForResizable();
            if (resizable != null)
            {
                resizable.Shrink();
            }
        }
    }

    void CheckForResizable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection.forward, out hit, 5f))
        {
            if (hit.transform.GetComponent<ResizableObject>() != null)
            {
                resizable = hit.transform.GetComponent<ResizableObject>();
            }
        }
    }
}
