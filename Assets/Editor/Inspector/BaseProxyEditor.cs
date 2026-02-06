using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseProxy), true)]
public class BaseProxyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BaseProxy proxy = (BaseProxy)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Proxy Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Enter"))
        {
            if (proxy.enabled) return;

            proxy.enabled = true;
            proxy.Enter();
        }

        if (GUILayout.Button("Exit"))
        {
            if (!proxy.enabled) return;

            proxy.Exit();
            proxy.enabled = false;
        }
    }
}
