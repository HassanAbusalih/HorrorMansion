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
        if (interactable.interactType == InteractType.Button)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("gameEvent"));
        }
        else if (interactable.interactType == InteractType.Text)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
