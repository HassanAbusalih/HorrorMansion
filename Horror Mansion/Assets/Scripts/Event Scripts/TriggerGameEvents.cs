using UnityEngine;

/// <summary>
/// Notifies a GameEvent when an object enters its trigger. It is possible to set this to only happen once.
/// </summary>

public class TriggerGameEvents : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outGoing;
    [SerializeField] bool singleInteraction;

    public GameEvent Notifier => outGoing;

    private void OnTriggerEnter(Collider other)
    {
        outGoing.Notify();

        if (singleInteraction)
        {
            Destroy(this);
        }
    }

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
