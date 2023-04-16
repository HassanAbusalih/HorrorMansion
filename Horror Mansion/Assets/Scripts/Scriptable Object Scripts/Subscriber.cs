using UnityEngine;
using System;

public interface ISubscriber : IGameEvent
{
    [SerializeField] public GameEvent Subscriber { get; }
    //string GetName { get; }
}

public interface IGameEvent 
{

}

