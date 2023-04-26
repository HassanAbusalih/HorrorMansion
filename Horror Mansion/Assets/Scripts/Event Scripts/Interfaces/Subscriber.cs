
public interface ISubscriber : IGameEvent
{
    public GameEvent Subscriber { get; }
    public string GetName();
}

public interface ISubscribers : IGameEvent
{
    public GameEvent[] Subscribers { get; }
}

public interface IGameEvent 
{
    
}

