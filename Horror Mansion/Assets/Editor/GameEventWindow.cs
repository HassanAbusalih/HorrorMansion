using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventWindow : EditorWindow
{
    List<GameEventInfo> mySubscribers = new();
    List<GameEventInfo> myNotifiers = new();
    public static List<GameEventInfo> subscribers = new();
    public static List<GameEventInfo> notifiers = new();
    Vector2 scrollBar;
    bool searchComplete;
    public static bool activate;
    bool showSubscribers;
    bool showNotifiers;

    [MenuItem("Window/Custom/Game Events")]
    public static void ShowWindow()
    {
        GetWindow<GameEventWindow>("Game Events");
    }

    private void OnGUI()
    {
        TryDrawingLines();
        scrollBar = EditorGUILayout.BeginScrollView(scrollBar);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.FlexibleSpace();
        var activateTemp = GUILayout.Toggle(activate, "Activate", EditorStyles.toolbarButton);
        if (activate != activateTemp)
        {
            Selection.selectionChanged();
        }
        activate = activateTemp;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (activate)
        {
            if (subscribers.Count > 0)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                GUILayout.FlexibleSpace();
                showSubscribers = EditorGUILayout.Foldout(showSubscribers, "Subscribers");
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                if (showSubscribers)
                {
                    DrawGameEvents("Subscribing to", subscribers);
                }    
            }
            if (notifiers.Count > 0)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                GUILayout.FlexibleSpace();
                showNotifiers = EditorGUILayout.Foldout(showNotifiers, "Notifiers");
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                if (showNotifiers)
                {
                    DrawGameEvents("Notifying to    ", notifiers); 
                }
            }
        }
        GUILayout.EndScrollView();
    }

    private void DrawGameEvents(string scriptLabel, List<GameEventInfo> gameEvents)
    {
        //EditorGUI.BeginDisabledGroup(true);
        foreach (var gameEvent in gameEvents)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Game Object  ", EditorStyles.label);
            EditorGUILayout.ObjectField(gameEvent.gameObject, typeof(GameObject), false);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label(scriptLabel, EditorStyles.label);
            EditorGUILayout.ObjectField(gameEvent.connectedTo, typeof(MonoBehaviour), false);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Script               ", EditorStyles.label);
            EditorGUILayout.ObjectField(gameEvent.monoBehaviour, typeof(MonoBehaviour), false);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(9);
            GUILayout.Label("Game Event    ", EditorStyles.label);
            EditorGUILayout.ObjectField(gameEvent.gameEvent, typeof(GameEvent), false);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
        //EditorGUI.EndDisabledGroup();
    }

    private void OnEnable()
    {
        Selection.selectionChanged += Refresh;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Refresh;
    }

    private void Refresh()
    {
        searchComplete = false;
        showSubscribers = false;
        showNotifiers = false; 
        subscribers.Clear();
        notifiers.Clear();
        myNotifiers.Clear();
        mySubscribers.Clear();
        Repaint();
        SceneView.RepaintAll();
    }

    public void TryDrawingLines()
    {
        if (!searchComplete && activate)
        {
            GetMyGameEvents();
            SearchForGameEvents();
            searchComplete = true;
        }
        else if (!activate)
        {
            subscribers.Clear();
            notifiers.Clear();
        }
    }

    public void GetMyGameEvents()
    {
        if (Selection.activeGameObject == null)
        {
            return;
        }
        foreach (MonoBehaviour monoBehaviour in Selection.activeGameObject.GetComponents<MonoBehaviour>())
        {
            if (monoBehaviour is IGameEvent)
            {
                if (monoBehaviour is ISubscriber)
                {
                    GameEventInfo gameEventInfo = new();
                    gameEventInfo.monoBehaviour = monoBehaviour;
                    gameEventInfo.gameEvent = (monoBehaviour as ISubscriber).Subscriber;
                    mySubscribers.Add(gameEventInfo);
                }
                if (monoBehaviour is INotifier)
                {
                    GameEventInfo gameEventInfo = new();
                    gameEventInfo.monoBehaviour = monoBehaviour;
                    gameEventInfo.gameEvent = (monoBehaviour as INotifier).Notifier;
                    myNotifiers.Add(gameEventInfo);
                }
            }
        }
    }

    public void SearchForGameEvents()
    {
        if (Selection.activeGameObject == null)
        {
            return;
        }    
        List<MonoBehaviour> events = new List<MonoBehaviour>();
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>(true);
        foreach (GameObject sceneObject in sceneObjects)
        {
            if (sceneObject == Selection.activeGameObject)
            {
                continue;
            }
            MonoBehaviour[] monoBehaviours = sceneObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                if (monoBehaviour is IGameEvent)
                {
                    events.Add(monoBehaviour);
                }
            }
        }
        AddToListByType(events);
    }

    public void AddToListByType(List<MonoBehaviour> events)
    {
        foreach (var monoBehaviour in events)
        {
            if ((monoBehaviour is ISubscriber || monoBehaviour is ISubscribers) && myNotifiers.Count > 0)
            {
                if ((monoBehaviour as ISubscriber).Subscriber != null)
                {
                    foreach (var notifier in myNotifiers)
                    {
                        if (notifier.gameEvent == (monoBehaviour as ISubscriber).Subscriber)
                        {
                            GameEventInfo gameEventInfo = new();
                            gameEventInfo.gameEvent = (monoBehaviour as ISubscriber).Subscriber;
                            gameEventInfo.gameObject = monoBehaviour.gameObject;
                            gameEventInfo.monoBehaviour = monoBehaviour;
                            gameEventInfo.connectedTo = notifier.monoBehaviour;
                            subscribers.Add(gameEventInfo);
                        }
                    }
                }
                else if ((monoBehaviour as ISubscribers).Subscribers.Length > 0)
                {
                    foreach(var notifier in myNotifiers)
                    {
                        foreach(var subscriber in (monoBehaviour as ISubscribers).Subscribers)
                        {
                            GameEventInfo gameEventInfo = new();
                            gameEventInfo.gameEvent = subscriber;
                            gameEventInfo.gameObject = monoBehaviour.gameObject;
                            gameEventInfo.monoBehaviour = monoBehaviour;
                            gameEventInfo.connectedTo = notifier.monoBehaviour;
                            subscribers.Add(gameEventInfo);
                        }
                    }
                }
            }
            if (monoBehaviour is INotifier && mySubscribers.Count > 0)
            {
                if ((monoBehaviour as INotifier).Notifier != null)
                {
                    foreach (var subscriber in mySubscribers)
                    {
                        if (subscriber.gameEvent == (monoBehaviour as INotifier).Notifier)
                        {
                            GameEventInfo gameEventInfo = new();
                            gameEventInfo.gameEvent = (monoBehaviour as INotifier).Notifier;
                            gameEventInfo.gameObject = monoBehaviour.gameObject;
                            gameEventInfo.monoBehaviour = monoBehaviour;
                            gameEventInfo.connectedTo = subscriber.monoBehaviour;
                            notifiers.Add(gameEventInfo);
                        }
                    }
                }
            }
        }
    }
}

public class GameEventInfo
{
    public GameObject gameObject;
    public GameEvent gameEvent;
    public MonoBehaviour connectedTo;
    public MonoBehaviour monoBehaviour;
}
