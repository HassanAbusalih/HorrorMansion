using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizedObjectReciever : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] string desiredObjectName;
    PickUpable resizedObject;

    private void OnTriggerEnter(Collider other)
    {
        resizedObject = other.GetComponent<PickUpable>();
        if (resizedObject != null && resizedObject.pickUpable && desiredObjectName == resizedObject.identifier)
        {
            gameEvent.NotifyInt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (resizedObject != null)
        {
            gameEvent.NotifyInt();
        }
    }
}
