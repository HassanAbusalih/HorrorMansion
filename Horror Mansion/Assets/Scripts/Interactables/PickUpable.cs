using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpable : MonoBehaviour
{
    [HideInInspector] public bool pickUpable = true;

    public void SetEvent(UnityEvent gameEvent)
    {
        gameEvent.AddListener(ToggleState);
        pickUpable = false;
    }

    void ToggleState()
    {
        pickUpable = !pickUpable;
    }
}
