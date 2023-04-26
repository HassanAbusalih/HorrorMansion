
public interface INotifier : IGameEvent
{
    public GameEvent Notifier { get; }
    public string GetName();
}
