using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// - Takes input from player
/// - Does a raycast to check if the player is looking at an InstantResizable
/// - If they are, a public method inside InstantResizable is called to resize the object up or down
/// </summary>
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
        if (Physics.Raycast(rayDirection.ViewportPointToRay(screenCenter), out RaycastHit hit, 5f, LayerMask.GetMask("Default")))
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
