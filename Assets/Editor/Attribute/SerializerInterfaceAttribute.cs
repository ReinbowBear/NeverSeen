using System;
using UnityEngine;
using UnityEditor;
using System.Linq;

[AttributeUsage(AttributeTargets.Field)]
public class SerializerInterfaceAttribute : PropertyAttribute
{
    //  [SerializeReference, SerializerInterface] public IMyInterface myInterface;
}


[CustomPropertyDrawer(typeof(SerializerInterfaceAttribute))]
public class SerializerInterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var interfaceType = fieldInfo.FieldType;
        if (!interfaceType.IsInterface) return;

        EditorGUI.BeginProperty(position, label, property);

        if (property.managedReferenceValue == null)
        {
            if (GUI.Button(position, $"Create {interfaceType.Name}"))
            {
                var implementations = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToArray();

                var menu = new GenericMenu();
                foreach (var impl in implementations)
                {
                    menu.AddItem(new GUIContent(impl.Name), false, () =>
                    {
                        property.managedReferenceValue = Activator.CreateInstance(impl);
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, property.managedReferenceValue.GetType().Name);
        }

        EditorGUI.EndProperty();
    }
}


//[CustomPropertyDrawer(typeof(SerializerInterfaceAttribute), true)]
//public class SerializerInterfaceDrawer : PropertyDrawer
//{
//    private Editor cachedEditor = null;
//
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        var interfaceType = fieldInfo.FieldType;
//        if (!interfaceType.IsInterface)
//        {
//            EditorGUI.LabelField(position, label.text, "Not an interface!");
//            return;
//        }
//
//        EditorGUI.BeginProperty(position, label, property);
//
//        // Определяем базовую высоту для кнопки/лейбла
//        float lineHeight = EditorGUIUtility.singleLineHeight;
//        Rect rect = new Rect(position.x, position.y, position.width, lineHeight);
//
//        // Если ещё нет объекта
//        if (property.managedReferenceValue == null)
//        {
//            if (GUI.Button(rect, $"Create {interfaceType.Name}"))
//            {
//                var implementations = AppDomain.CurrentDomain.GetAssemblies()
//                    .SelectMany(a => a.GetTypes())
//                    .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
//                    .ToArray();
//
//                GenericMenu menu = new GenericMenu();
//                foreach (var impl in implementations)
//                {
//                    menu.AddItem(new GUIContent(impl.Name), false, () =>
//                    {
//                        property.managedReferenceValue = Activator.CreateInstance(impl);
//                        property.serializedObject.ApplyModifiedProperties();
//                    });
//                }
//                menu.ShowAsContext();
//            }
//        }
//        else
//        {
//            // Если объект есть, показываем его тип
//            EditorGUI.LabelField(rect, label.text, property.managedReferenceValue.GetType().Name);
//
//            // Создаём встроенный Editor для выбранного объекта
//            if (property.managedReferenceValue != null)
//            {
//                if (cachedEditor == null || cachedEditor.target != property.managedReferenceValue)
//                {
//                    cachedEditor = Editor.CreateEditor(property.managedReferenceValue as UnityEngine.Object);
//                    // Если это не UnityEngine.Object, используем InternalEditorUtility
//                    cachedEditor = Editor.CreateEditor(new SerializedObjectWrapper(property));
//                }
//
//                if (cachedEditor != null)
//                {
//                    // Отрисовываем вложенные поля с отступом
//                    EditorGUI.indentLevel++;
//                    cachedEditor.OnInspectorGUI();
//                    EditorGUI.indentLevel--;
//                }
//            }
//        }
//
//        EditorGUI.EndProperty();
//    }
//
//    // Высота поля (учитываем вложенные поля)
//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        float lineHeight = EditorGUIUtility.singleLineHeight;
//        float height = lineHeight;
//
//        if (property.managedReferenceValue != null)
//        {
//            // Простейший способ — немного увеличим высоту, потом можно улучшить через SerializedObject
//            height += lineHeight * 5; // зарезервировано место для полей
//        }
//
//        return height;
//    }
//}
