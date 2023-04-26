using UnityEngine;

/// <summary>
/// Pulls in the specified interactable to be aligned with the center of its object, and notifies a GameEvent the first time the interactable is received.
/// The interactable can still be picked up and placed again, but the GameEvent will not be notified again.
/// </summary>

public class ObjectReceiver : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outgoing;
    public GameEvent Notifier => outgoing;
    string INotifier.GetName() => nameof(outgoing);
    [SerializeField] Interactable objectToReceive;
    bool objectReceived;
    bool notified;

    void Update()
    {
        if (objectReceived && objectToReceive.canInteract)
        {
            SnapIntoPosition(objectToReceive.transform);
        }
    }

    void SnapIntoPosition(Transform objectToSnap)
    {
        if (transform.position.x != objectToSnap.transform.position.x && transform.position.y != objectToSnap.transform.position.y)
        {
            Vector3 direction = transform.position - objectToSnap.position;
            if (direction.magnitude > 0.1f)
            {
                direction = direction.normalized * 5;
                objectToSnap.position += new Vector3(direction.x * Time.deltaTime, 0, direction.z * Time.deltaTime);
            }
            if (transform.rotation != objectToSnap.rotation)
            {
                objectToSnap.rotation = Quaternion.RotateTowards(objectToSnap.rotation, transform.rotation, 5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (other.gameObject == objectToReceive.gameObject)
        {
            if (!notified)
            {
                outgoing.Notify();
                notified = true;
            }
            objectReceived = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;
        if (other.gameObject == objectToReceive.gameObject)
        {
            objectReceived = false;
        }
    }
}
