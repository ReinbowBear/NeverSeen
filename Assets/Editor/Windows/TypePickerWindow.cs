using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TypePickerWindow : EditorWindow
{
    private SerializedObject serializedObject;
    private string propertyPath;

    private string search;
    private Type[] types;

    private Vector2 scroll;

    public void Init(SerializedProperty prop)
    {
        serializedObject = prop.serializedObject;
        propertyPath = prop.propertyPath;

        types = TypeCache.GetTypesDerivedFrom<object>()
            .Where(type => !type.IsAbstract && !type.IsInterface && string.IsNullOrEmpty(type.Namespace) && type.Assembly.GetName().Name == "Assembly-CSharp")
            .OrderBy(t => t.FullName)
            .ToArray();
    }

    private void OnGUI()
    {
        DrawSearch();
        scroll = EditorGUILayout.BeginScrollView(scroll);
        DrawList();
        EditorGUILayout.EndScrollView();
    }

    private void DrawSearch()
    {
        EditorGUILayout.LabelField("Type Picker", EditorStyles.boldLabel);
        search = EditorGUILayout.TextField("Search", search);
    }

    private void DrawList()
    {
        var filtered = types.Where(type =>string.IsNullOrEmpty(search) ||
        type.FullName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

        foreach (var type in filtered)
        {
            if (GUILayout.Button(type.FullName, EditorStyles.miniButton))
            {
                ApplyType(type);
                Close();
            }
        }
    }

    private void ApplyType(Type type)
    {
        serializedObject.Update();

        var prop = serializedObject.FindProperty(propertyPath);
        if (prop != null)
        {
            prop.stringValue = type.AssemblyQualifiedName;
            serializedObject.ApplyModifiedProperties();
        }
    }
}
