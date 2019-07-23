using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToggleSkybox))]
public class ToggleSkyboxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Set Daytime Skybox", EditorStyles.miniButton))
        {
            ((ToggleSkybox)target).SetDaytimeSkybox();

            Undo.RecordObject(target, "SetDaytimeSkybox");
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Set Night Time Skybox", EditorStyles.miniButton))
        {
            ((ToggleSkybox)target).SetNightTimeSkybox();

            Undo.RecordObject(target, "SetNightTimeSkybox");
            EditorUtility.SetDirty(target);
        }

        DrawDefaultInspector();
    }
}
