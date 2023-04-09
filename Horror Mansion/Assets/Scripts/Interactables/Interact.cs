using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField] KeyCode interactButton = KeyCode.E;
    [SerializeField] Transform pickUpPosition;
    [SerializeField] TextMeshProUGUI interactText;
    Interactable interactable;
    Interactable heldInteractable;
    SmoothResizingGun smoothResizingGun;
    InstantResizingGun instantResizingGun;
    Transform rayDirection;
    Rigidbody rb;

    void Start()
    {
        rayDirection = FindFirstObjectByType<FirstPersonCam>().transform;
        smoothResizingGun = GetComponent<SmoothResizingGun>();
        instantResizingGun = GetComponent<InstantResizingGun>();
    }

    void Update()
    {
        CheckForInteractable();
        if (Input.GetKeyDown(interactButton))
        {
            if (heldInteractable != null)
            {
                DropObject();
            }
            else if (interactable != null && interactable.canInteract)
            {
                InteractWithObject();
            }
        }
    }

    private void CheckForInteractable()
    {
        if (Physics.Raycast(rayDirection.position, rayDirection.forward, out RaycastHit hit, 5f, LayerMask.GetMask("Default")))
        {
            interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null && heldInteractable == null)
            {
                interactText.gameObject.SetActive(interactable.canInteract);
                return;
            }
        }
        interactText.gameObject.SetActive(false);
        interactable = null;
    }

    private void InteractWithObject()
    {
        if (interactable.interactType == InteractType.PickUp)
        {
            PickUpObject();
        }
        if (interactable.interactType == InteractType.Text)
        {
            //add description functionality, make interactable null
            //interactable.description
        }
        if (interactable.interactType == InteractType.Button)
        {
            //add button functionality, make interactable null
        }
    }

    private void PickUpObject()
    {
        heldInteractable = interactable;
        heldInteractable.canInteract = false;
        heldInteractable.transform.position = pickUpPosition.position;
        heldInteractable.transform.SetParent(transform, true);
        rb = heldInteractable.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        if (smoothResizingGun != null)
        {
            smoothResizingGun.enabled = false;
        }
        if (instantResizingGun != null)
        {
            instantResizingGun.enabled = false;
        }
    }

    void DropObject()
    {
        heldInteractable.transform.SetParent(null, true);
        heldInteractable.canInteract = true;
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        heldInteractable = null;
        rb = null;
        if (smoothResizingGun != null)
        {
            smoothResizingGun.enabled = true;
        }
        if (instantResizingGun != null)
        {
            instantResizingGun.enabled = true;
        }
    }
}
