using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // делаем недоступным

        string valueLabel;

        if (property.propertyType == SerializedPropertyType.Generic)
        {
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
            valueLabel = property.displayName + ": " + property.stringValue;
        }

        EditorGUI.LabelField(position, label.text, valueLabel);

        GUI.enabled = true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
