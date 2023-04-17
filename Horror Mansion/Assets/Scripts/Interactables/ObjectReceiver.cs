using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectReceiver : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outgoing;
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
        if (other.gameObject == objectToReceive.gameObject)
        {
            objectReceived = false;
        }
    }
}
