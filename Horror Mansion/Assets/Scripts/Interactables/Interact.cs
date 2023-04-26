using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This handles all possible interactions that an Interactable requires, when the player is at a certain range and 
/// presses the designated key.
/// This includes picking up objects, displaying descriptions, or triggering GameEvents for buttons.
/// It also handles making text pop up when a player is looking at an interactable.
/// </summary>
public class Interact : MonoBehaviour
{
    [SerializeField] KeyCode interactButton = KeyCode.E;
    [SerializeField] Transform pickUpPosition;
    [SerializeField] Image interactText;
    Interactable interactable;
    Interactable heldInteractable;
    SmoothResizingGun smoothResizingGun;
    InstantResizingGun instantResizingGun;
    Camera rayDirection;
    Vector3 screenCenter = new(0.5f, 0.5f);
    Rigidbody rb;
    bool deactivated;

    void Start()
    {
        rayDirection = FindFirstObjectByType<Camera>();
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
                if (!interactable.enabled) { return; }
                InteractWithObject();
            }
        }
    }

    private void FixedUpdate()
    {
        if (heldInteractable != null)
        {
            Vector3 direction = pickUpPosition.position - heldInteractable.transform.position;
            if (direction.magnitude > 0.1f)
            {
                direction = direction.normalized * 5;
                Vector3 moveDirection = new();
                moveDirection += new Vector3(direction.x, 0, direction.z);
                rb.velocity = moveDirection;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayDirection.ViewportPointToRay(screenCenter), out hit, 2.5f, LayerMask.GetMask("Default"))
            || Physics.SphereCast(rayDirection.ViewportPointToRay(screenCenter), 0.2f, out hit, 2.5f, LayerMask.GetMask("Default")))
        {
            if (hit.transform.TryGetComponent<Interactable>(out interactable))
            {
                if (!interactable.enabled)
                {
                    return;
                }
                if (heldInteractable == null)
                {
                    interactText.gameObject.SetActive(interactable.canInteract);
                    return;
                }
            }
        }
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
        interactable = null;
    }

    private void InteractWithObject()
    {
        switch (interactable.interactType)
        {
            case InteractType.PickUp:
                PickUpObject();
                break;
            case InteractType.Text:
                interactable.ShowDescription();
                interactable = null;
                break;
            case InteractType.Button:
                interactable.PushButton();
                interactable = null;
                break;
            default:
                break;
        }
    }

    private void PickUpObject()
    {
        heldInteractable = interactable;
        heldInteractable.canInteract = false;
        heldInteractable.transform.position = pickUpPosition.position;
        heldInteractable.transform.up = transform.up;
        heldInteractable.transform.SetParent(transform, true);
        rb = heldInteractable.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        if (smoothResizingGun != null)
        {
            if (smoothResizingGun.enabled)
            {
                deactivated = false;
                smoothResizingGun.enabled = false;
            }
            else
            {
                deactivated = true;
            }
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
            rb.constraints = RigidbodyConstraints.None;
        }
        else
        {
            rb = heldInteractable.gameObject.AddComponent<Rigidbody>();
        }
        Vector3 throwVector = Vector3.Lerp(heldInteractable.playerPos.forward, heldInteractable.playerPos.up, 0.3f);
        if (heldInteractable.pickUp == null)
        {
            heldInteractable.pickUp = new();
        }
        rb.AddForce(0.5f * heldInteractable.pickUp.ThrowForce * throwVector, ForceMode.Impulse);
        heldInteractable = null;
        rb = null;
        if (smoothResizingGun != null)
        {
            if (deactivated)
            {
                return;
            }

            smoothResizingGun.enabled = true;
        }
        if (instantResizingGun != null)
        {
            instantResizingGun.enabled = true;
        }
    }
}
