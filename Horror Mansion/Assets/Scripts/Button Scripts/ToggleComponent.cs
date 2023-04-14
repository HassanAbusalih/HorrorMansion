using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleComponent : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] MonoBehaviour componentToToggle;
    [SerializeField] bool singleInteraction;

    void Start()
    {
        gameEvent.Subscribe(Toggle);
    }

    private void Toggle()
    {
        componentToToggle.enabled = !componentToToggle.enabled;
        if (singleInteraction)
        {
            gameEvent.Unsubscribe(Toggle);
        }
    }

}
