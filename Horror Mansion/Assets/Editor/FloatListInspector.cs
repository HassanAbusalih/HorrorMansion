using UnityEngine;
using UnityEditor;

/// <summary>
/// This script changes the inspector for FloatLists by replacing the foldout that Unity creates for lists with regular fields for each FloatVar in the FloatList.
/// A button is placed at the bottom to create new entries in the list.
/// </summary>

[CustomEditor(typeof(FloatList))]
public class FloatListInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        FloatList floatList = (FloatList)target;
        Undo.RecordObject(floatList, "Float List change");
        EditorGUILayout.LabelField("Float Variables");
        for (int i = 0; i < floatList.floatVars.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            floatList.floatVars[i].name = EditorGUILayout.TextField(floatList.floatVars[i].name);
            floatList.floatVars[i].value = EditorGUILayout.FloatField(floatList.floatVars[i].value);
            if (GUILayout.Button("-"))
            {
                floatList.floatVars.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("New", GUILayout.Width(80)))
        {
            floatList.floatVars.Add(new FloatVar());
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}
