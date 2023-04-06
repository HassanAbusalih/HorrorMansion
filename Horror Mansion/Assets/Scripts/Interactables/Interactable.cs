using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public bool canInteract = true;
    public InteractType interactType = InteractType.None;
    [TextArea(5, 10)] public string description;
    public GameEvent gameEvent;

    private void Awake()
    {
        if (interactType == InteractType.PickUp && GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }

    public void SetEvent(UnityEvent gameEvent)
    {
        gameEvent.AddListener(ToggleState);
        interactType = InteractType.PickUp;
        canInteract = false;
    }

    void ToggleState()
    {
        canInteract = !canInteract;
    }
}

public enum InteractType
{
    None,
    PickUp,
    Text,
    Button
}