using UnityEngine;

/// <summary>
/// Subscribes to a GameEvent, and toggles a MonoBehaviour on or off when that GameEvent is notified.
/// </summary>

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
