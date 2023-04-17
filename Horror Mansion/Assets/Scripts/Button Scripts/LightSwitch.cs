using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    string ISubscriber.GetName() => nameof(incoming);
    [SerializeField] Material materialWhenOn;
    [SerializeField] Material materialWhenOff;
    Material currentMaterial;
    Light lightsouce;

    private void Start()
    {
        incoming.Subscribe(SwitchState);
        lightsouce = GetComponent<Light>();
        currentMaterial = GetComponent<Renderer>().material;
    }

    void SwitchState()
    {
        if (lightsouce != null)
        {
            lightsouce.enabled = !lightsouce.enabled;
            if (lightsouce.enabled)
            {
                currentMaterial = materialWhenOn;
            }
            else
            {
                currentMaterial = materialWhenOff;
            }
        }
        else
        {
            Debug.Log($"Attach a light onto {gameObject.name}!");
        }
    }
}
