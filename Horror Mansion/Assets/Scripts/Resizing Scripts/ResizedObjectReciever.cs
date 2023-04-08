using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizedObjectReciever : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] string desiredObjectName;
    [SerializeField] bool isPartOfSet;
    Component resizedObject;

    private void OnTriggerEnter(Collider other)
    {
        resizedObject = other.GetComponent<InstantResizable>();
        if (resizedObject == null)
        {
            resizedObject = other.GetComponent<SmoothResizable>();
        }
        if (resizedObject != null && resizedObject.name == desiredObjectName)
        {
            if (isPartOfSet)
            {
                gameEvent.NotifyStorageObj();
            }
            else
            {
                gameEvent.Notify();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (resizedObject != null && isPartOfSet)
        {
            gameEvent.NotifyStorageObj();
        }
    }
}
