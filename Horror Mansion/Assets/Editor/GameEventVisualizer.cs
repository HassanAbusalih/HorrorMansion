using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
public class GameEventVisualizer : Editor
{
    public static GameEventVisualizer Instance;
    public List<SerializedObject> subscribers = new();
    public List<SerializedObject> notifiers = new();
    SerializedProperty subscriber;
    SerializedProperty notifier;
    bool searchComplete;
    public static bool activate;
    Vector3 startTangent;
    Vector3 endTangent;

    private void OnSceneGUI()
    {
        if (activate)
        {
            TryDrawingLines();
        }
        else
        {
            GameEventWindow.subscribers.Clear();
            GameEventWindow.notifiers.Clear();
        }
    }

    public void TryDrawingLines()
    {
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
        }
        if (subscribers.Count > 0)
        {
            GameEventWindow.subscribers = subscribers;
            DrawGameEventCurves(subscribers, Color.green);
        }
        if (notifiers.Count > 0)
        {
            GameEventWindow.notifiers = notifiers;
            DrawGameEventCurves(notifiers, Color.red);
        }
    }

    public void GetMyGameEvents()
    {
        if (serializedObject.targetObject is ISubscriber)
        {
            subscriber = serializedObject.FindProperty(((ISubscriber)serializedObject.targetObject).GetName());
        }
        if (serializedObject.targetObject is INotifier)
        {
            notifier = serializedObject.FindProperty(((INotifier)serializedObject.targetObject).GetName());
        }
    }
    public void SearchForGameEvents()
    {
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>(true);
        foreach (GameObject sceneObject in sceneObjects)
        {
            MonoBehaviour[] monoBehaviours = sceneObject.GetComponentsInChildren<MonoBehaviour>(true);
            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                if (monoBehaviour == serializedObject.targetObject)
                {
                    break;
                }
                if (monoBehaviour is IGameEvent)
                {
                    AddToListByType(monoBehaviour);
                }
            }
        }
    }
    public void AddToListByType(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour is ISubscriber && notifier != null)
        {
            SerializedObject serializedObject = new SerializedObject(monoBehaviour);
            SerializedProperty property = serializedObject.FindProperty(((ISubscriber)monoBehaviour).GetName());
            if (property != null && property.objectReferenceValue == notifier.objectReferenceValue)
            {
                subscribers.Add(serializedObject);
            }
        }
        if (monoBehaviour is INotifier && subscriber != null)
        {
            SerializedObject serializedObject = new SerializedObject(monoBehaviour);
            SerializedProperty property = serializedObject.FindProperty(((INotifier)monoBehaviour).GetName());
            if (property != null && property.objectReferenceValue == subscriber.objectReferenceValue)
            {
                notifiers.Add(serializedObject);
            }
        }
        searchComplete = true;
    }
    public void DrawGameEventCurves(List<SerializedObject> gameEvents, Color color)
    {
        foreach (var gameEvent in gameEvents)
        {
            Transform myObject = ((MonoBehaviour)serializedObject.targetObject).gameObject.transform;
            Transform targetObject = ((MonoBehaviour)gameEvent.targetObject).gameObject.transform;
            startTangent = Vector3.Slerp(myObject.position, targetObject.position, 0.5f);
            endTangent = Vector3.Min(targetObject.position, myObject.position);
            Handles.DrawBezier(myObject.transform.position, targetObject.transform.position, startTangent, endTangent, color, null, 4);
        }
    }
}
