using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Notifier : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    public void Notify() => gameEvent.Notify();

    public void NotifyObj(object obj) => gameEvent.NotifyObj(obj);

    public void NotifyStorageObj() => gameEvent.NotifyStorageObj();
}
