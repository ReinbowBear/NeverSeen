using System;
using UnityEditor;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{ 
    // [SerializeField, ReadOnly] private int debugValue; // будет отображатся в инспекторе но будет нередактируемым
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}



public class ProgressBarAttribute : PropertyAttribute
{
    public readonly float Min;
    public readonly float Max;
    public readonly string Label;

    public ProgressBarAttribute(float min, float max, string label = null)
    {
        Min = min;
        Max = max;
        Label = label;
    }
}

[CustomPropertyDrawer(typeof(ProgressBarAttribute))]
public class ProgressBarDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ProgressBarAttribute progress = attribute as ProgressBarAttribute;

        if (property.propertyType != SerializedPropertyType.Float && property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Только int или float в значениях!");
            return;
        }

        float value = property.propertyType == SerializedPropertyType.Float ? property.floatValue : property.intValue;
        float min = progress.Min;
        float max = progress.Max;

        float percent = Mathf.InverseLerp(min, max, value);

        string barLabel = progress.Label ?? label.text;
        barLabel += $" ({value}/{max})";

        EditorGUI.ProgressBar(position, percent, barLabel);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}



[AttributeUsage(AttributeTargets.Field)]
public class SerializeInterfaceAttribute : PropertyAttribute
{
    public Type TargetType;

    public SerializeInterfaceAttribute(Type targetType)
    {
        TargetType = targetType;
    }
}

[CustomPropertyDrawer(typeof(SerializeInterfaceAttribute))]
public class SerializeInterfaceDrawer : PropertyDrawer // [SerializeReference, SerializeInterface(typeof(IWeapon))]
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (SerializeInterfaceAttribute)attribute;
        Type interfaceType = attr.TargetType;

        EditorGUI.BeginProperty(position, label, property);

        UnityEngine.Object assignedObject = property.managedReferenceValue as UnityEngine.Object;

        UnityEngine.Object newObject = EditorGUI.ObjectField(position, label, assignedObject, typeof(UnityEngine.Object), true);

        if (newObject != assignedObject)
        {
            if (newObject == null)
            {
                property.managedReferenceValue = null;
            }
            else
            {
                var newType = newObject.GetType();
                if (interfaceType.IsAssignableFrom(newType))
                {
                    property.managedReferenceValue = newObject;
                }
                else
                {
                    Debug.LogWarning($"Класс не наследуется от: {interfaceType.Name}");
                }
            }
        }

        EditorGUI.EndProperty();
    }
}
