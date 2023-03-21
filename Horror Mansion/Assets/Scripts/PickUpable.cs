using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpable : MonoBehaviour
{
    [HideInInspector] public bool pickUpable = true;
    [HideInInspector] public bool isResized;
    [HideInInspector] public FloatList identifier;

    public void SetEvent(UnityEvent gameEvent, FloatList identity)
    {
        identifier = identity;
        gameEvent.AddListener(ToggleState);
        pickUpable = false;
    }

    void ToggleState()
    {
        pickUpable = !pickUpable;
        isResized = pickUpable;
    }
}
