using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ToggleComponent : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    public GameEvent Subscriber => incoming;
    string ISubscriber.GetName() => nameof(incoming);
    [SerializeField] MonoBehaviour componentToToggle;
    [SerializeField] bool singleInteraction;

    void Start()
    {
        incoming.Subscribe(Toggle);
    }

    private void Toggle()
    {
        componentToToggle.enabled = !componentToToggle.enabled;
        if (singleInteraction)
        {
            incoming.Unsubscribe(Toggle);
        }
    }

}
