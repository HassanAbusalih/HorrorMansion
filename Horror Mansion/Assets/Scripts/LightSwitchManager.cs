using UnityEngine;

public class LightSwitchManager : MonoBehaviour, INotifier
{
    [SerializeField] LightSwitch[] lights;
    [SerializeField] GameEvent outgoing;
    public GameEvent Notifier => outgoing;
    public string GetName() => nameof(outgoing);
    bool solved;
    bool active;

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
            outgoing.Notify();
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent( out FirstPersonCam placeholder))
        {
            active = true;
        }
    }
}
