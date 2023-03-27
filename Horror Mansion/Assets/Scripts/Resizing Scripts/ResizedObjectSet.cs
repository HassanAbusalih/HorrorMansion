using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ResizedObjectSet : MonoBehaviour
{
    [SerializeField] List<GameEvent> objectRecievers;
    [SerializeField] GameEvent puzzleComplete;
    bool[] addedObjects;
    int objectsToTrack;
    int currentNumber;

    void Start()
    {
        addedObjects = new bool[objectRecievers.Count];
        objectsToTrack = objectRecievers.Count;
        for(int i = 0; i < objectRecievers.Count; i++)
        {
            objectRecievers[i].SubscribeInt(Increment, i);
            Debug.Log("Subscribed with " + i);
        }
    }

    void Increment(int number)
    {
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
