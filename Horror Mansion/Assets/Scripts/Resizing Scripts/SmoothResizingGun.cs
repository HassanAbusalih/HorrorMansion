using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// - Takes input from player
/// - Does a raycast to check if the player is looking at a SmoothResizable
/// - If they are, a public method inside SmoothResizable is called to resize the object up or down
/// </summary>

public class SmoothResizingGun : MonoBehaviour
{
    [SerializeField] KeyCode enlargeButton = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkButton = KeyCode.Mouse1;
    [SerializeField] FloatList resizeSpeed;
    SmoothResizable resizable;
    Vector3 screenCenter = new (0.5f, 0.5f);
    Camera rayDirection;

    private void Start()
    {
        rayDirection = FindObjectOfType<Camera>();
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
        if (Physics.Raycast(rayDirection.ViewportPointToRay(screenCenter), out RaycastHit hit, 5f, LayerMask.GetMask("Default")))
        {
            resizable = hit.transform.GetComponent<SmoothResizable>();
            if (resizable != null)
            {
                return;
            }
        }
        resizable = null;
    }
}
