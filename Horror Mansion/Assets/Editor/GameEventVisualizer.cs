using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(MonoBehaviour), true)]
public class GameEventVisualizer : Editor
{
    private void OnEnable()
    {
        EditorSceneManager.sceneDirtied += OnSceneDirty;

    }
    public void OnSceneDirty(Scene scene) => OnSceneGUI();
    private void OnDisable()
    {
        EditorSceneManager.sceneDirtied -= OnSceneDirty;

    }
    private void OnSceneGUI()
    {
        if (GameEventWindow.activate)
        {
            if (GameEventWindow.subscribers.Count > 0)
            {
                DrawGameEventCurves(GameEventWindow.subscribers, Color.green);
            }
            if (GameEventWindow.notifiers.Count > 0)
            {
                DrawGameEventCurves(GameEventWindow.notifiers, Color.red);
            }
        }
    }

    public void DrawGameEventCurves(List<GameEventInfo> gameEvents, Color color)
    {
        foreach (var gameEvent in gameEvents)
        {
            Transform myObject = Selection.activeGameObject.transform;
            Transform targetObject = gameEvent.gameObject.transform;
            Vector3 startTangent = Vector3.Slerp(myObject.position, targetObject.position, 0.5f);
            Vector3 endTangent = Vector3.Min(targetObject.position, myObject.position);
            Handles.DrawBezier(myObject.transform.position, targetObject.transform.position, startTangent, endTangent, color, null, 4);
        }
    }
}
