using System;
using UnityEngine;

/// <summary>
/// A scriptable object that allows communication between scripts using the events contained within it.
/// </summary>

[CreateAssetMenu(fileName = "New Game Event", menuName = "Custom/Game Event")]

public class GameEvent : ScriptableObject
{
    public event Action VoidEvent;
    public event Action<object> ObjEvent;
    public TStorage<object> storageObjEvent = new();

    public void Subscribe(Action action) => VoidEvent += action;
    public void Unsubscribe(Action action) => VoidEvent -= action;
    public void Notify() => VoidEvent?.Invoke();

    public void SubscribeObj(Action<object> action) => ObjEvent += action;
    public void UnsubscribeObj(Action<object> action) => ObjEvent -= action;
    public void NotifyObj(object obj) => ObjEvent?.Invoke(obj);

    public void SubscribeStorageObj(Action<object> action, object obj)
    {
        storageObjEvent.Store(obj);
        storageObjEvent.StorageEvent += action;
    }

    public void UnsubscribeStorageObj(Action<object> action)
    {
        storageObjEvent.obj = null;
        storageObjEvent.StorageEvent -= action;
    }
    public void NotifyStorageObj() => storageObjEvent.RaiseEvent();
}

/// <summary>
/// A generic class that stores a variable and later triggers an event that takes that variable as a parameter.
/// </summary>
/// <typeparam name="T"> The type of the object to be stored. </typeparam>
public class TStorage<T>
{
    public T obj;
    public event Action<T> StorageEvent;

    public void Store(T obj)
    {
        this.obj = obj;
    }

    public void RaiseEvent()
    {
        StorageEvent?.Invoke(obj);
    }
}
