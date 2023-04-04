using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Custom/Game Event")]
public class GameEvent : ScriptableObject
{
    public event Action voidEvent;
    public event Action<object> objEvent;
    public TStorage<object> storageObjEvent = new();

    public void Notify()
    {
        voidEvent.Invoke();
    }

    public void Subscribe(Action action)
    {
        voidEvent += action;
    }

    public void SubscribeObj(Action<object> action)
    {
        objEvent += action;
    }

    public void NotifyObj(object obj)
    {
        objEvent.Invoke(obj);
    }

    public void SubscribeStorageObj(Action<object> action, object number)
    {
        storageObjEvent.Store(number);
        storageObjEvent.storageEvent += action;
    }

    public void NotifyStorageObj()
    {
        storageObjEvent.RaiseEvent();
    }
}

public class TStorage<T>
{
    T storage;
    public event Action<T> storageEvent;

    public void Store(T obj)
    {
        storage = obj;
    }

    public void RaiseEvent()
    {
        storageEvent.Invoke(storage);
    }
}
