using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableInspector : Editor
{
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
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("text").FindPropertyRelative("description"));
    }

    private void ShowButtonProperties(Interactable interactable)
    {
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("button").FindPropertyRelative("gameEvent"));
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
