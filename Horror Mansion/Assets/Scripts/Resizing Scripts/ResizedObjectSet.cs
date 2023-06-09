using System.Collections.Generic;
using UnityEngine;

public class ResizedObjectSet : MonoBehaviour
{
    [SerializeField] List<GameEvent> resizedObjects = new();
    [SerializeField] GameEvent puzzleComplete;
    bool puzzleSolved;
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
        if (objectsToTrack == currentNumber && !puzzleSolved)
        {
            puzzleComplete.Notify();
            puzzleSolved = true;
        }
    }
    
}
