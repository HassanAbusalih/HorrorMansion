using UnityEngine;
using System;

public interface ISubscriber : IGameEvent
{
    [SerializeField] public GameEvent Subscriber { get; }
    public string GetName();
}

public interface IGameEvent 
{

}

