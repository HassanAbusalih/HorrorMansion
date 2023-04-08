using UnityEngine;

[System.Serializable]
public class Notifier
{
    [SerializeField] GameEvent gameEvent;
    public void Notify() => gameEvent.Notify();
    public void NotifyObj(object obj) => gameEvent.NotifyObj(obj);
    public void NotifyStorageObj() => gameEvent.NotifyStorageObj();
}
