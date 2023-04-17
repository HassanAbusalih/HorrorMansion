using UnityEngine;
using System;

public interface ISubscriber : IGameEvent
{
    public string GetName();
}

public interface IGameEvent 
{
    
}

