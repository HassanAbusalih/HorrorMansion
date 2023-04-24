using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
