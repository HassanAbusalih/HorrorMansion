using UnityEngine;

/// <summary>
/// Has an array of LightSwitches which it continually loops over to check if they are all active. If they are, a GameEvent is notified and the script is disabled.
/// The looping does not start until the player walks into the trigger.
/// </summary>

public class LightSwitchManager : MonoBehaviour, INotifier
{
    [SerializeField] LightSwitch[] lights;
    [SerializeField] GameEvent outgoing;
    public GameEvent Notifier => outgoing;
    public string GetName() => nameof(outgoing);
    bool solved;
    [SerializeField] bool active;

    void Update()
    {
        if (active)
        {
            CheckIfSolved();
        }
    }

    private void CheckIfSolved()
    {
        if (!solved)
        {
            foreach (var light in lights)
            {
                if (!light.Active)
                {
                    return;
                }
            }
            solved = true;
        }
        if (solved)
        {
            Debug.Log("Solved!");
            outgoing.Notify();
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController placeholder))
        {
            active = true;
        }
    }
}
