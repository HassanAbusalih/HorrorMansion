using UnityEngine;

/// <summary>
/// When the player enters this object's trigger, it activates the PickUpText gameobject. If the player then presses E, it notifies a GameEvent and enables the ResizeGun, while also
/// disabling PickUpText and its gameobject. If the player leaves the trigger without pressing E, only the PickUpText is deactivated.
/// </summary>

public class PickUpGun : MonoBehaviour
{
    public GameObject ResizeGun;
    public GameObject PickUpText;
    [SerializeField] GameEvent gameEvent;
    private bool inTrigger;

    void Start()
    {
        ResizeGun.SetActive(false);
        PickUpText.SetActive(false);
    }

    private void Update()
    {
        if (inTrigger && Input.GetKey(KeyCode.E))
        {
            ResizeGun.SetActive(true);
            gameEvent.Notify();
            PickUpText.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            inTrigger = true;
            PickUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
        inTrigger = false;
    }
}