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
