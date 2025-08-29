using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class ConstructorInputWindow : EditorWindow
{
    private ParameterInfo[] parameters;
    private object[] inputValues;
    private Action<object[]> onConfirm;
    private Type targetType;

    public void Init(Type targetType, ParameterInfo[] parameters, Action<object[]> onConfirm)
    {
        this.targetType = targetType;
        this.parameters = parameters;
        this.onConfirm = onConfirm;
        inputValues = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            // Значения по умолчанию
            inputValues[i] = parameters[i].ParameterType.IsValueType
                ? Activator.CreateInstance(parameters[i].ParameterType)
                : null;
        }

        minSize = new Vector2(300, parameters.Length * 25 + 60);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField($"Создание {targetType.Name}", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        for (int i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(param.Name, GUILayout.Width(100));

            Type paramType = param.ParameterType;
            object value = inputValues[i];

            // Поддерживаем базовые типы
            if (paramType == typeof(float))
                inputValues[i] = EditorGUILayout.FloatField((float)(value ?? 0f));
            else if (paramType == typeof(int))
                inputValues[i] = EditorGUILayout.IntField((int)(value ?? 0));
            else if (paramType == typeof(string))
                inputValues[i] = EditorGUILayout.TextField((string)(value ?? ""));
            else if (paramType == typeof(bool))
                inputValues[i] = EditorGUILayout.Toggle((bool)(value ?? false));
            else
                EditorGUILayout.LabelField($"Тип {paramType.Name} не поддерживается");

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Создать"))
        {
            onConfirm?.Invoke(inputValues);
            Close();
        }
    }
}
