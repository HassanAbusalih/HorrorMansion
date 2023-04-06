using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ResizedObjectSet : MonoBehaviour
{
    [SerializeField] List<Subscriber> resizedObjects = new();
    [SerializeField] Notifier puzzleComplete;
    bool[] addedObjects;
    int objectsToTrack;
    int currentNumber;

    void Start()
    {
        addedObjects = new bool[resizedObjects.Count];
        objectsToTrack = resizedObjects.Count;
        for(int i = 0; i < resizedObjects.Count; i++)
        {
            resizedObjects[i].SubscribeStorageObj(Increment, i);
            Debug.Log("Subscribed with " + i);
        }
    }

    void Increment(object i)
    {
        int number = (int)i;
        if (addedObjects[number])
        {
            currentNumber--;
            addedObjects[number] = false;
        }
        else
        {
            currentNumber++;
            addedObjects[number] = true;
        }
        Debug.Log(currentNumber);
        if (objectsToTrack == currentNumber)
        {
            puzzleComplete.Notify();
        }
    }
    
}
