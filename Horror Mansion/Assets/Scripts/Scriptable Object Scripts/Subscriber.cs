using UnityEngine;
using System;

public interface ISubscriber : IGameEvent
{
    GameEvent Subscriber { get; }
}

public interface IGameEvent
{

}

