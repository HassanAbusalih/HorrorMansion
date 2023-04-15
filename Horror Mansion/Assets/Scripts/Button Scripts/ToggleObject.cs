using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] GameObject objectToToggle;
    [SerializeField] bool singleInteraction;

    private void Start()
    {
        gameEvent.Subscribe(Toggle);
    }

    void Toggle()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
        if (singleInteraction)
        {
            gameEvent.Unsubscribe(Toggle);
        }
    }

}
