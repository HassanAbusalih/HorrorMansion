using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizedObjectReciever : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] string desiredObjectName;
    [Min(0.01f)][SerializeField] float desiredObjectMinSize;
    [SerializeField] float desiredObjectMaxSize;
    [SerializeField] bool isPartOfSet;
    Resizable resizedObject;
    Interactable interactable;
    Vector3 direction;
    float distance;
    float resizedObjectSize = 0;
    bool puzzleSolved;

    private void Start()
    {
        gameObject.layer = 1;
    }

    private void Update()
    {
        if (resizedObject != null)
        {
            resizedObjectSize = resizedObject.currentSize;
            direction = transform.position - resizedObject.transform.position;
            distance = direction.magnitude;
            if (distance > 0.1f && interactable.canInteract)
            {
                resizedObject.transform.position += new Vector3(direction.normalized.x * Time.deltaTime, 0, direction.normalized.z * Time.deltaTime);

            }
            if (transform.rotation != resizedObject.transform.rotation && interactable.canInteract)
            {
                resizedObject.transform.rotation = Quaternion.RotateTowards(resizedObject.transform.rotation, transform.rotation, 0.1f);
            }
            if (resizedObjectSize <= desiredObjectMaxSize && resizedObjectSize >= desiredObjectMinSize)
            {
                SolvePuzzle();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InstantResizable instantResizable = other.GetComponent<InstantResizable>();
        SmoothResizable smoothResizable = other.GetComponent<SmoothResizable>();
        Interactable interactable = other.GetComponent<Interactable>();
        if (instantResizable != null && instantResizable.name == desiredObjectName)
        {
            resizedObject = instantResizable;
            resizedObjectSize = instantResizable.currentSize;
            this.interactable = interactable;
        }
        else if (smoothResizable != null && smoothResizable.name == desiredObjectName)
        {
            resizedObject = smoothResizable;
            resizedObjectSize = smoothResizable.currentSize;
            this.interactable = interactable;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (resizedObject != null && isPartOfSet)
        {
            gameEvent.NotifyStorageObj();
        }
        resizedObject = null;
    }

    private void SolvePuzzle()
    {
        if (isPartOfSet)
        {
            gameEvent.NotifyStorageObj();
        }
        else if (!puzzleSolved)
        {
            gameEvent.Notify();
            Debug.Log("it works");
            puzzleSolved = true;
        }
        resizedObject = null;
    }

}
