using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTestScript : MonoBehaviour, ISubscriber, INotifier
{
    [SerializeField] private GameEvent incoming;
    [SerializeField] private GameEvent outgoing;
    [SerializeField] public GameEvent Notifier { get { return outgoing; } }
    [SerializeField] string getName => incoming.ToString();
}
