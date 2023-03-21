using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] KeyCode pickUpButton = KeyCode.E;
    [SerializeField] Transform pickUpTransform;
    PickUpable pickUpObject;
    AlternateResizingGun alternateResizing;
    ResizingGun resizingGun;
    Transform rayDirection;
    Rigidbody rb;

    void Start()
    {
        rayDirection = FindObjectOfType<FirstPersonCam>().transform;
        alternateResizing = GetComponent<AlternateResizingGun>();
        resizingGun = GetComponent<ResizingGun>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pickUpButton))
        {
            if (pickUpObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(rayDirection.position, rayDirection.forward, out hit, 5f))
                {
                    pickUpObject = hit.transform.GetComponent<PickUpable>();
                    if (pickUpObject != null && pickUpObject.pickUpable)
                    {
                        pickUpObject.transform.position = pickUpTransform.position;
                        pickUpObject.transform.SetParent(transform, true);
                        rb = pickUpObject.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = true;
                        }
                        if (alternateResizing != null)
                        {
                            alternateResizing.enabled = false;
                        }
                        if (resizingGun != null)
                        {
                            resizingGun.enabled = false;
                        }
                    }
                }
            }
            else if (pickUpObject != null)
            {
                pickUpObject.transform.SetParent(null, true);
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
                pickUpObject = null;
                rb = null;
                if (alternateResizing != null)
                {
                    alternateResizing.enabled = true;
                }
                if (resizingGun != null)
                {
                    resizingGun.enabled = true;
                }
            }
        }
    }
}
