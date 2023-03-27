using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Custom/Game Event")]
public class GameEvent : ScriptableObject
{
    public event Action voidEvent;
    public TStorage<int> intEvent = new();

    public void Notify()
    {
        voidEvent.Invoke();
    }

    public void Subscribe(Action action)
    {
        voidEvent += action;
    }

    public void SubscribeInt(Action<int> action, int number)
    {
        intEvent.Store(number);
        intEvent.storageEvent += action;
    }

    public void NotifyInt()
    {
        intEvent.RaiseEvent();
    }
}

public class TStorage<T>
{
    T storage;
    public event Action<T> storageEvent;

    public void Store(T number)
    {
        storage = number;
    }

    public void RaiseEvent()
    {
        storageEvent.Invoke(storage);
    }
}
