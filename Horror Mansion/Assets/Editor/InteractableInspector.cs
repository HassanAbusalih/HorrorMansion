using UnityEngine;
using UnityEditor;

/// <summary>
/// This script changes the inspector for interactables to only display the properties/variables relevant to each type of interactable. It also inherits from GameEventVisualizer
/// to allow the tool to work on interactables (buttons, specifically).
/// </summary>

[CustomEditor(typeof(Interactable))]
public class InteractableInspector : GameEventVisualizer
{
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

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Interactable interactable = (Interactable)target;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("interactType"));
        EditorGUILayout.Separator();
        if (interactable.interactType == InteractType.Button)
        {
            ShowButtonProperties(interactable);
        }
        else if (interactable.interactType == InteractType.Text)
        {
            ShowTextProperties();
        }
        else if (interactable.interactType == InteractType.PickUp)
        {
            ShowPickUpProperties();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void ShowPickUpProperties()
    {
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pickUp").FindPropertyRelative("throwForce"));
    }

    private void ShowTextProperties()
    {
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("text").FindPropertyRelative("textPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("text").FindPropertyRelative("destroyTime"));
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("text").FindPropertyRelative("description"));
    }

    private void ShowButtonProperties(Interactable interactable)
    {
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameEvent"));
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("variableNeeded"));
        if (interactable.button.VariableNeeded)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("componentNeeded"));
            if (interactable.button.ComponentNeeded)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("componentToPassIn"));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("stringToPassIn"));
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("animated"));
        if (interactable.button.Animated)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("animator"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("animationName"));
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("singleInteraction"));
    }
}
