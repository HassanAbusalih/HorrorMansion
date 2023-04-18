using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    public GameEvent Subscriber => incoming;
    string ISubscriber.GetName() => nameof(incoming);
    [SerializeField] GameObject objectToToggle;
    [SerializeField] bool singleInteraction;

    private void Start()
    {
        incoming.Subscribe(Toggle);
    }

    void Toggle()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
        if (singleInteraction)
        {
            incoming.Unsubscribe(Toggle);
        }
    }

}
