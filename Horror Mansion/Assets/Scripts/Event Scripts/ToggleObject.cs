using UnityEngine;

/// <summary>
/// Subscribes to a GameEvent, and toggles a GameObject on or off when the GameEvent is notified.
/// </summary>

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
