using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameEventWindow : EditorWindow
{
    GameEventVisualizer visualizer;
    bool turnOn;
    [MenuItem("Window/Custom/Game Event Visualizer")]
    public static void ShowWindow()
    {
        GetWindow<GameEventWindow>("Game Events");
    }

    [System.Obsolete]
    private void OnGUI()
    {
        visualizer = GameEventVisualizer.Instance;
        if (visualizer == null)
        {
            Debug.Log("gave up");
            return;
        }
        
    }
}
