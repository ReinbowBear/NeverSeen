using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeReference))]
public class TypeReferenceDrawer : PropertyDrawer
{
    private const float ButtonWidth = 80f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var typeProp = property.FindPropertyRelative("typeName");

        Type current = Type.GetType(typeProp.stringValue);

        Rect labelRect = new Rect(position.x, position.y, position.width - ButtonWidth, position.height);
        Rect buttonRect = new Rect(position.x + position.width - ButtonWidth, position.y, ButtonWidth, position.height);

        EditorGUI.LabelField(labelRect, label.text, current != null ? current.Name : "None");

        if (GUI.Button(buttonRect, "Select"))
        {
            ShowPopup(typeProp);
        }
    }

    private void ShowPopup(SerializedProperty typeProp)
    {
        var window = ScriptableObject.CreateInstance<TypePickerWindow>();
        window.Init(typeProp);
        window.ShowUtility();
    }
}
