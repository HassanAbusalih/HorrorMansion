using UnityEngine;

/// <summary>
/// Takes an array of GameEvents that all subscribe to its SwitchState method. When a GameEvent is notified, it's current state is toggled and its material is changed accordingly.
/// Also has a public getter to show it's current state.
/// </summary>

[RequireComponent (typeof(Light))]
public class LightSwitch : MonoBehaviour
{
    [SerializeField] GameEvent[] incoming;
    [SerializeField] Material materialWhenOn;
    [SerializeField] Material materialWhenOff;
    Renderer myRenderer;
    Light lightSource;
    public bool Active => lightSource.enabled;
    private void Start()
    {
        foreach(var gameEvent in incoming)
        {
            gameEvent.Subscribe(SwitchState);
        }

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
