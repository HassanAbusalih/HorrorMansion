using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class GameEventWindow : EditorWindow
{
    public static List<SerializedObject> subscribers = new();
    public static List<SerializedObject> notifiers = new();

    [MenuItem("Window/Custom/Game Events")]
    public static void ShowWindow()
    {
        GetWindow<GameEventWindow>("Game Events");
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.FlexibleSpace();
        GameEventVisualizer.activate = GUILayout.Toggle(GameEventVisualizer.activate, "Activate", EditorStyles.toolbarButton);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (subscribers.Count > 0)
        {
            DrawGameEvents("Subscribers", subscribers);
        }
        if (notifiers.Count > 0)
        {
            DrawGameEvents("Notifiers", notifiers);
        }
    }

    private static void DrawGameEvents(string labelName, List<SerializedObject> gameEvents)
    {
        GUILayout.Space(5);
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.FlexibleSpace();
        GUILayout.Label(new GUIContent(labelName), EditorStyles.label);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        EditorGUI.BeginDisabledGroup(true);
        foreach (var gameEvent in gameEvents)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.ObjectField((gameEvent.targetObject as MonoBehaviour).gameObject, typeof(GameObject), false);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        EditorGUI.EndDisabledGroup();
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
        Repaint();
    }
}
