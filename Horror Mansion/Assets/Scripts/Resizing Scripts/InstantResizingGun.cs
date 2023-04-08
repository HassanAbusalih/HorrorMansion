using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantResizingGun : MonoBehaviour
{
    [SerializeField] KeyCode enlargeButton = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkButton = KeyCode.Mouse1;
    InstantResizable resizable;
    Vector3 screenCenter = new (0.5f, 0.5f);
    Camera rayDirection;

    private void Start()
    {
        rayDirection = FindObjectOfType<Camera>();
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
        if (Physics.Raycast(rayDirection.ViewportPointToRay(screenCenter), out RaycastHit hit, 5f))
        {
            resizable = hit.transform.GetComponent<InstantResizable>();
            if (resizable != null)
            {
                return;
            }
        }
        resizable = null;
    }
}
