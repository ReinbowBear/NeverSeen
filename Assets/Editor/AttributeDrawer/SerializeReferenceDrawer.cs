using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

#region ATTRIBUTES

[AttributeUsage(AttributeTargets.Field)]
public class SerializeReferenceSelectorAttribute : PropertyAttribute
{
}

[AttributeUsage(AttributeTargets.Field)]
public class TypeSelectorAttribute : PropertyAttribute
{
    public Type BaseType;

    public TypeSelectorAttribute(Type baseType)
    {
        BaseType = baseType;
    }
}

#endregion

#region SERIALIZE REFERENCE DRAWER

[CustomPropertyDrawer(typeof(SerializeReferenceSelectorAttribute))]
public class SerializeReferenceSelectorDrawer : PropertyDrawer
{
    private const float ButtonWidth = 90f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect fieldRect = GetFieldRect(position);
        Rect buttonRect = GetButtonRect(position);

        DrawPropertyField(fieldRect, property, label);
        DrawTypeSelectionButton(buttonRect, property);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    #region DRAW

    private void DrawPropertyField(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(rect, property, label, true);
    }

    private void DrawTypeSelectionButton(Rect rect, SerializedProperty property)
    {
        if (GUI.Button(rect, "Select"))
        {
            ShowTypeMenu(property);
        }
    }

    #endregion

    #region MENU

    private void ShowTypeMenu(SerializedProperty property)
    {
        GenericMenu menu = new GenericMenu();

        Type baseType = GetFieldType();

        if (baseType == null)
        {
            menu.AddDisabledItem(new GUIContent("No Type"));
            menu.ShowAsContext();
            return;
        }

        AddNullOption(menu, property);
        AddDerivedTypes(menu, property, baseType);

        menu.ShowAsContext();
    }

    private void AddNullOption(GenericMenu menu, SerializedProperty property)
    {
        menu.AddItem(
            new GUIContent("Null"),
            property.managedReferenceValue == null,
            () =>
            {
                property.serializedObject.Update();
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties();
            });
    }

    private void AddDerivedTypes(GenericMenu menu, SerializedProperty property, Type baseType)
    {
        var types = TypeCache
            .GetTypesDerivedFrom(baseType)
            .Where(IsValidType)
            .OrderBy(t => t.Name);

        foreach (Type type in types)
        {
            AddTypeMenuItem(menu, property, type);
        }
    }

    private void AddTypeMenuItem(GenericMenu menu, SerializedProperty property, Type type)
    {
        bool selected =
            property.managedReferenceValue != null &&
            property.managedReferenceValue.GetType() == type;

        menu.AddItem(
            new GUIContent(type.FullName),
            selected,
            () =>
            {
                AssignType(property, type);
            });
    }

    #endregion

    #region TYPE LOGIC

    private Type GetFieldType()
    {
        return fieldInfo.FieldType;
    }

    private bool IsValidType(Type type)
    {
        return
            !type.IsAbstract &&
            !type.IsInterface &&
            !type.IsGenericType;
    }

    private void AssignType(SerializedProperty property, Type type)
    {
        property.serializedObject.Update();

        object instance = Activator.CreateInstance(type);

        property.managedReferenceValue = instance;

        property.serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #region RECTS

    private Rect GetFieldRect(Rect position)
    {
        return new Rect(
            position.x,
            position.y,
            position.width - ButtonWidth - 4f,
            position.height);
    }

    private Rect GetButtonRect(Rect position)
    {
        return new Rect(
            position.x + position.width - ButtonWidth,
            position.y,
            ButtonWidth,
            EditorGUIUtility.singleLineHeight);
    }

    #endregion
}

#endregion

#region TYPE SELECTOR DRAWER

[CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
public class TypeSelectorDrawer : PropertyDrawer
{
    private const float ButtonWidth = 90f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TypeSelectorAttribute attributeData =
            (TypeSelectorAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        Rect labelRect = GetLabelRect(position);
        Rect buttonRect = GetButtonRect(position);

        DrawLabel(labelRect, property, label);
        DrawSelectButton(buttonRect, property, attributeData.BaseType);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    #region DRAW

    private void DrawLabel(Rect rect, SerializedProperty property, GUIContent label)
    {
        string currentTypeName = GetCurrentTypeName(property);

        EditorGUI.LabelField(
            rect,
            label.text,
            currentTypeName);
    }

    private void DrawSelectButton(Rect rect, SerializedProperty property, Type baseType)
    {
        if (GUI.Button(rect, "Select"))
        {
            ShowTypeMenu(property, baseType);
        }
    }

    #endregion

    #region MENU

    private void ShowTypeMenu(SerializedProperty property, Type baseType)
    {
        GenericMenu menu = new GenericMenu();

        AddNoneOption(menu, property);
        AddAvailableTypes(menu, property, baseType);

        menu.ShowAsContext();
    }

    private void AddNoneOption(GenericMenu menu, SerializedProperty property)
    {
        menu.AddItem(
            new GUIContent("None"),
            string.IsNullOrEmpty(property.stringValue),
            () =>
            {
                property.stringValue = string.Empty;
                property.serializedObject.ApplyModifiedProperties();
            });
    }

    private void AddAvailableTypes(
        GenericMenu menu,
        SerializedProperty property,
        Type baseType)
    {
        var types = TypeCache
            .GetTypesDerivedFrom(baseType)
            .Where(IsValidType)
            .OrderBy(t => t.Name);

        foreach (Type type in types)
        {
            AddTypeOption(menu, property, type);
        }
    }

    private void AddTypeOption(
        GenericMenu menu,
        SerializedProperty property,
        Type type)
    {
        bool selected = property.stringValue == type.AssemblyQualifiedName;

        menu.AddItem(
            new GUIContent(type.FullName),
            selected,
            () =>
            {
                AssignType(property, type);
            });
    }

    #endregion

    #region TYPE LOGIC

    private bool IsValidType(Type type)
    {
        return
            !type.IsAbstract &&
            !type.IsInterface &&
            !type.IsGenericType;
    }

    private void AssignType(SerializedProperty property, Type type)
    {
        property.stringValue = type.AssemblyQualifiedName;
        property.serializedObject.ApplyModifiedProperties();
    }

    private string GetCurrentTypeName(SerializedProperty property)
    {
        if (string.IsNullOrEmpty(property.stringValue))
        {
            return "None";
        }

        Type type = Type.GetType(property.stringValue);

        return type != null
            ? type.Name
            : "Missing";
    }

    #endregion

    #region RECTS

    private Rect GetLabelRect(Rect position)
    {
        return new Rect(
            position.x,
            position.y,
            position.width - ButtonWidth - 4f,
            position.height);
    }

    private Rect GetButtonRect(Rect position)
    {
        return new Rect(
            position.x + position.width - ButtonWidth,
            position.y,
            ButtonWidth,
            EditorGUIUtility.singleLineHeight);
    }

    #endregion
}

#endregion
