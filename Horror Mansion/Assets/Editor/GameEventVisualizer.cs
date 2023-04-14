using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IGameEvent))]
public class GameEventVisualizer : Editor
{
    List<SerializedProperty> subscribers = new();
    List<SerializedProperty> notifiers = new();
    SerializedProperty subscriber;
    SerializedProperty notifier;

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
                    AddToList(component);
                }
            }
        }
    }

    private void AddToList(Component component)
    {
        if (component is ISubscriber && notifier != null)
        {
            SerializedProperty property = new SerializedObject(component).FindProperty("Subscriber");
            if (property == subscriber)
            {
                subscribers.Add(property);
            }
        }
        if (component is INotifier && subscriber != null)
        {
            SerializedProperty property = new SerializedObject(component).FindProperty("Notifier");
            if (property == notifier)
            {
                notifiers.Add(property);
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
