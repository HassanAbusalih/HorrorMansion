using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizedObjectReciever : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] FloatList desiredObjectFloatList;
    PickUpable resizedObject;

    private void OnTriggerEnter(Collider other)
    {
        resizedObject = other.GetComponent<PickUpable>();
        if (resizedObject != null && resizedObject.isResized && desiredObjectFloatList == resizedObject.identifier)
        {
            gameEvent.Notify();
        }
    }
}
