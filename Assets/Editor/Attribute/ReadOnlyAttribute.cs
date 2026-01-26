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
