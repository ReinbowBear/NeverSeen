using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SerializeReference), true)]
public class SerializeReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.isArray)
        {
            DrawArray(property, label, ref position);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        EditorGUI.EndProperty();
    }

    private void DrawArray(SerializedProperty arrayProp, GUIContent label, ref Rect position)
    {
        EditorGUI.LabelField(position, label.text + $" ({arrayProp.arraySize})");
        position.y += EditorGUIUtility.singleLineHeight;

        for (int i = 0; i < arrayProp.arraySize; i++)
        {
            var element = arrayProp.GetArrayElementAtIndex(i);
            float elementHeight = EditorGUI.GetPropertyHeight(element, true);
            Rect elementRect = new Rect(position.x, position.y, position.width, elementHeight);
            EditorGUI.PropertyField(elementRect, element, new GUIContent($"Element {i}"), true);
            position.y += elementHeight;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;

        if (property.isArray)
        {
            for (int i = 0; i < property.arraySize; i++)
            {
                var element = property.GetArrayElementAtIndex(i);
                height += EditorGUI.GetPropertyHeight(element, true);
            }
        }
        else if (property.managedReferenceValue != null)
        {
            height = EditorGUI.GetPropertyHeight(property, true);
        }

        return height;
    }
}
