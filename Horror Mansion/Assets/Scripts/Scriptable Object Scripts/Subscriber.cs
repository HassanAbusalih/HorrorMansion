using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Subscriber : MonoBehaviour
{
    public GameEvent gameEvent;
    public void Subscribe(Action action) => gameEvent.VoidEvent += action;

    public void SubscribeObj(Action<object> action) => gameEvent.ObjEvent += action;

    public void SubscribeStorageObj(Action<object> action, object obj)
    {
        gameEvent.storageObjEvent.Store(obj);
        gameEvent.storageObjEvent.StorageEvent += action;
    }

    public void Unsubscribe(Action action) => gameEvent.VoidEvent -= action;

    public void UnsubscribeObj(Action<object> action) => gameEvent.ObjEvent -= action;

    public void UnsubscribeStorageObj(Action<object> action)
    {
        gameEvent.storageObjEvent.obj = null;
        gameEvent.storageObjEvent.StorageEvent -= action;
    }
}

public class ISubscriber
{
    
}

