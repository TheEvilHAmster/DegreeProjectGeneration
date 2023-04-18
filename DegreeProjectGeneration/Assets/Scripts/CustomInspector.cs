using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapManager manager = (MapManager)target;
        if (GUILayout.Button("ReGenerate"))
        {
            manager.ReGenerate();
        }

    }
}
