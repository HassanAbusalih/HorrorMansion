using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IGameEvent))]
public class GameEventVisualizer : Editor
{
    List<SerializedObject> subscribers = new();
    List<SerializedObject> notifiers = new();
    SerializedProperty subscriber;
    SerializedProperty notifier;

    private void OnSceneGUI()
    {
        if (subscriber == null && notifier == null)
        {
            SearchForGameEvents();
        }
        else if (subscribers == null && notifiers == null)
        {
            Debug.Log("Didn't work!");
            return;
        }
        foreach(var subscriber in subscribers)
        {
            GameObject myObject = (GameObject)serializedObject.targetObject;
            GameObject targetObject = (GameObject)subscriber.targetObject;
            Handles.DrawLine(myObject.transform.position, targetObject.transform.position);
        }
    }

    void SearchForGameEvents()
    {
        GetMyGameEvents();
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>(true);
        foreach (GameObject sceneObject in sceneObjects)
        {
            Component[] components = sceneObject.GetComponentsInChildren<Component>(true);
            foreach (Component component in components)
            {
                if (component is IGameEvent)
                {
                    AddToListByType(component);
                }
            }
        }
    }

    private void AddToListByType(Component component)
    {
        if (component is ISubscriber && notifier != null)
        {
            SerializedObject serializedObject = new SerializedObject(component);
            if (serializedObject.FindProperty("Subscriber") == subscriber)
            {
                subscribers.Add(serializedObject);
            }
        }
        if (component is INotifier && subscriber != null)
        {
            SerializedObject serializedObject = new SerializedObject(component);
            if (serializedObject.FindProperty("Notifier") == notifier)
            {
                notifiers.Add(serializedObject);
            }
        }
    }

    private void GetMyGameEvents()
    {
        if (serializedObject.targetObject is ISubscriber)
        {
            subscriber = serializedObject.FindProperty("Subscriber");
        }
        if (serializedObject.targetObject is INotifier)
        {
            notifier = serializedObject.FindProperty("Notifier");
        }
    }
}
