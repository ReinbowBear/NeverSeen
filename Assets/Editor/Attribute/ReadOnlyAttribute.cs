using UnityEditor;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{ 
    // [SerializeField, ReadOnly] private int debugValue; // будет отображатся в инспекторе но будет нередактируемым
}

//[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
//public class ReadOnlyDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        GUI.enabled = false;
//        EditorGUI.PropertyField(position, property, label, true);
//        GUI.enabled = true;
//    }
//}


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // делаем недоступным

        string valueLabel;

        if (property.propertyType == SerializedPropertyType.Generic)
        {
            // Если это класс/структура/managedReference
            if (property.managedReferenceValue != null)
            {
                valueLabel = property.managedReferenceValue.GetType().Name;
            }
            else
            {
                valueLabel = "null";
            }
        }
        else
        {
            // Простейшие типы
            valueLabel = property.displayName + ": " + property.stringValue;
        }

        EditorGUI.LabelField(position, label.text, valueLabel);

        GUI.enabled = true; // возвращаем возможность редактировать другие поля
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}