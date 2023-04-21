using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    public GameEvent Subscriber => incoming;
    string ISubscriber.GetName() => nameof(incoming);
    [SerializeField] Material materialWhenOn;
    [SerializeField] Material materialWhenOff;
    Renderer myRenderer;
    Light lightSource;
    public bool Active => lightSource.enabled;
    private void Start()
    {
        incoming.Subscribe(SwitchState);
        lightSource = GetComponent<Light>();
        myRenderer = GetComponent<Renderer>();
    }

    void SwitchState()
    {
        if (lightSource != null)
        {
            lightSource.enabled = !lightSource.enabled;
            if (lightSource.enabled)
            {
                myRenderer.material = materialWhenOn;
            }
            else
            {
                myRenderer.material = materialWhenOff;
            }
        }
        else
        {
            Debug.Log($"Attach a light onto {gameObject.name}!");
        }
    }
}
