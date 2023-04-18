using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResizedObjectReciever : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outgoing;
    string INotifier.GetName() => nameof(outgoing);
    [SerializeField] string desiredObjectName;
    [Min(0.01f)][SerializeField] float desiredObjectMinSize;
    [SerializeField] float desiredObjectMaxSize;
    [SerializeField] bool isPartOfSet;
    Resizable resizedObject;
    Interactable interactable;
    float resizedObjectSize = 0;
    bool puzzleSolved;
    public GameEvent Notifier => outgoing;
    private void Start()
    {
        gameObject.layer = 1;
    }

    private void Update()
    {
        if (resizedObject != null)
        {
            resizedObjectSize = resizedObject.currentSize;
            if (interactable.canInteract)
            {
                SnapIntoPosition(resizedObject.transform);
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
            outgoing.NotifyStorageObj();
        }
        resizedObject = null;
    }

    private void SolvePuzzle()
    {
        if (isPartOfSet)
        {
            outgoing.NotifyStorageObj();
        }
        else if (!puzzleSolved)
        {
            outgoing.Notify();
            puzzleSolved = true;
        }
    }

    void SnapIntoPosition(Transform objectToSnap)
    {
        if (transform.position.x != objectToSnap.transform.position.x && transform.position.y != objectToSnap.transform.position.y)
        {
            Vector3 direction = transform.position - objectToSnap.position;
            if (direction.magnitude > 0.1f)
            {
                direction = direction.normalized * 5;
                objectToSnap.position += new Vector3(direction.x * Time.deltaTime, 0, direction.z * Time.deltaTime);
            }
            if (transform.rotation != objectToSnap.rotation)
            {
                objectToSnap.rotation = Quaternion.RotateTowards(objectToSnap.rotation, transform.rotation, 5f);
            }
        }
    }
}
