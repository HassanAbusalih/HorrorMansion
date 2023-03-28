using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpable : MonoBehaviour
{
    [HideInInspector] public bool pickUpable = true;
    [HideInInspector] public string identifier;

    public void SetEvent(UnityEvent gameEvent, string identity)
    {
        identifier = identity;
        gameEvent.AddListener(ToggleState);
        pickUpable = false;
    }

    void ToggleState()
    {
        pickUpable = !pickUpable;
    }
}
