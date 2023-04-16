using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
public class GameEventVisualizer : Editor
{
    public static GameEventVisualizer Instance;
    public  static readonly List<SerializedObject> subscribers = new();
    public static readonly List<SerializedObject> notifiers = new();
    SerializedProperty subscriber;
    SerializedProperty notifier;
    bool searchComplete;
    public static bool turnOn;
    Vector3 startTangent;
    Vector3 endTangent;

    private void OnSceneGUI()
    {
        if (Instance != this) { Instance = this; }
        //if (!turnOn) { return; }
        if (target is not IGameEvent)
        {
            return;
        }
        GetMyGameEvents();
        if (subscriber == null && notifier == null)
        {
            return;
        }
        if (subscribers.Count == 0 && notifiers.Count == 0 && !searchComplete)
        {
            SearchForGameEvents();
            //GUILayout.BeginArea(new Rect(10, 10, 10, 10));
        }
        if (subscribers.Count > 0)
        {
            DrawSubscriberCurves();
        }
        if (notifiers.Count > 0)
        {
            DrawNotifierCurves();
        }
    }
    private void GetMyGameEvents()
    {
        if (serializedObject.targetObject is ISubscriber)
        {
            //string eventName = serializedObject.FindProperty("GetName").stringValue;
            subscriber = serializedObject.FindProperty("incoming");
        }
        if (serializedObject.targetObject is INotifier)
        {
            notifier = serializedObject.FindProperty("outgoing");
        }
    }
    void SearchForGameEvents()
    {
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>(true);
        foreach (GameObject sceneObject in sceneObjects)
        {
            Component[] components = sceneObject.GetComponentsInChildren<Component>(true);
            foreach (Component component in components)
            {
                if (component == serializedObject.targetObject)
                {
                    break;
                }
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
            SerializedProperty property = serializedObject.FindProperty("incoming");
            if (property != null && property.objectReferenceValue == notifier.objectReferenceValue)
            {
                subscribers.Add(serializedObject);
            }
        }
        if (component is INotifier && subscriber != null)
        {
            SerializedObject serializedObject = new SerializedObject(component);
            SerializedProperty property = serializedObject.FindProperty("outgoing");
            if (property != null && property.objectReferenceValue == subscriber.objectReferenceValue)
            {
                notifiers.Add(serializedObject);
            }
        }
        searchComplete = true;
    }
    private void DrawSubscriberCurves()
    {
        foreach (var subscriber in subscribers)
        {
            Transform myObject = ((MonoBehaviour)serializedObject.targetObject).gameObject.transform;
            Transform targetObject = ((MonoBehaviour)subscriber.targetObject).gameObject.transform;
            startTangent = Vector3.Cross(myObject.position.normalized, targetObject.position.normalized) * 0.1f;
            endTangent = Vector3.Lerp(myObject.position, targetObject.position, 0.25f);
            Handles.DrawBezier(myObject.transform.position, targetObject.transform.position, endTangent, startTangent, Color.green, null, 5);
        }
    }
    private void DrawNotifierCurves()
    {
        foreach (var notifier in notifiers)
        {
            Transform myObject = ((MonoBehaviour)serializedObject.targetObject).gameObject.transform;
            Transform targetObject = ((MonoBehaviour)notifier.targetObject).gameObject.transform;
            startTangent = Vector3.Cross(myObject.position, targetObject.position) * 0.1f;
            endTangent = Vector3.Lerp(myObject.position, targetObject.position, 0.25f);
            Handles.DrawBezier(myObject.transform.position, targetObject.transform.position, 0.5f * startTangent, endTangent, Color.red, null, 5);
        }
    }
}
