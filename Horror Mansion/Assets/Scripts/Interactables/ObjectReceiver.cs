using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReceiver : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] GameObject objectToReceive;
    bool objectReceived;
    bool notified;

    void Update()
    {
        if (objectReceived)
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
        if (other.gameObject == objectToReceive)
        {
            if (!notified)
            {
                gameEvent.Notify();
                notified = true;
            }
            objectReceived = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToReceive)
        {
            objectReceived = false;
        }
    }
}
