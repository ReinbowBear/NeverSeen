using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ConfigSO))]
public class ConfigSOEditor : Editor
{
    private ReorderableList list;
    private static List<Type> behaviorTypes;

    private void OnEnable() // OnEnable вызывается до отрисовки инспектора объекта
    {
        string propertyName = "behaviorConfigs";
        SerializedProperty prop = serializedObject.FindProperty(propertyName);

        if (prop == null)
        {
            Debug.LogWarning($"В ScriptableObject {nameof(ConfigSO)} не найдено поле! : {propertyName}");
            return;
        }

        list = new ReorderableList(serializedObject, prop, true, true, true, true);

        list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, propertyName);
        };

        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            rect.x += 5;
            rect.width -= 10;

            string label = element.managedReferenceFullTypename;
            label = GetTypeName(label);

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, new GUIContent(label), true);
        };

        list.elementHeightCallback = (index) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(element) + 6;
        };

        list.onAddDropdownCallback = (rect, list) =>
        {
            if (behaviorTypes == null)
            {
                behaviorTypes = GetAllBehaviorTypes();
            }

            GenericMenu menu = new GenericMenu();

            foreach (var type in behaviorTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () =>
                {
                    var instance = Activator.CreateInstance(type);
                    prop.arraySize++;

                    var newElement = prop.GetArrayElementAtIndex(prop.arraySize - 1);
                    newElement.managedReferenceValue = instance;

                    serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        };
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //DrawDefaultInspector();
        DrawAllPropertiesExcept(serializedObject, "m_Script"); // m_Script имя поля скрипта, скриптбл обджекта, исключаем его из сереализации
        EditorGUILayout.Space();

        list.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawAllPropertiesExcept(SerializedObject obj, string propertyToSkip)
    {
        SerializedProperty prop = obj.GetIterator();
        bool enterChildren = true;
        while (prop.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (prop.name == propertyToSkip) continue;
            EditorGUILayout.PropertyField(prop, true);
        }

        // показывает переменные в инспекторе но серыми, можно вставить m_Script
        // но я не уверен что код должен быть здесь и в такой последовательности

        //SerializedProperty myValue = serializedObject.FindProperty("someVariable");
        //EditorGUI.BeginDisabledGroup(true);
        //EditorGUILayout.PropertyField(myValue);
        //EditorGUI.EndDisabledGroup();
    }


    private string GetTypeName(string fullTypeName)
    {
        if (string.IsNullOrEmpty(fullTypeName)) return "Null";

        var parts = fullTypeName.Split(' ');
        if (parts.Length != 2) return fullTypeName;

        var type = Type.GetType(parts[1]);
        return type != null ? type.Name : fullTypeName;
    }

    private List<Type> GetAllBehaviorTypes()
    {
        var list = new List<Type>();

        foreach (var type in TypeCache.GetTypesDerivedFrom<IBehavior>())
        {
            if (!type.IsAbstract && type.IsClass && type.IsSerializable)
            {
                list.Add(type);
            }
        }
        return list;
    }


    public static void ResetCache()
    {
        behaviorTypes = null;
    }
}
