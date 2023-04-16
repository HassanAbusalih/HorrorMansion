using UnityEngine;

public interface INotifier : IGameEvent
{
    GameEvent Notifier { get; }
    public string GetName();
}
