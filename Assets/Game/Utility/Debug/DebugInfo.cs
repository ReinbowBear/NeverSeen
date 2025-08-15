#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private Text infoText;
    [SerializeField] private Camera cam;

    private GameObject selectedObject;
    private HashSet<string> expandedPaths = new HashSet<string>();
    private HashSet<object> visitedObjects = new HashSet<object>();

    private void SelectObject(InputAction.CallbackContext _) => StartCoroutine(DoSelectObject());
    private IEnumerator DoSelectObject()
    {
        Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
        yield return null;

        if (EventSystem.current.IsPointerOverGameObject()) yield break;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            selectedObject = hit.collider.gameObject;
            expandedPaths.Clear();
            RefreshDebugInfo();
        }
        else
        {
            selectedObject = null;
            RefreshDebugInfo();
        }
    }

    private void RefreshDebugInfo()
    {
        if (selectedObject == null) return;

        visitedObjects.Clear();
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Selected: {selectedObject.name}");
        stringBuilder.AppendLine(new string('-', 30));

        foreach (var component in selectedObject.GetComponents<Component>())
        {
            string path = component.GetType().Name;

            AppendFoldout(stringBuilder, path, () => { stringBuilder.AppendLine(GetFieldsInfo(component, 1, path)); });
            stringBuilder.AppendLine();
        }

        infoText.text = stringBuilder.ToString();

        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            ToggleComponents();
        }
    }

    private void ToggleComponents(InputAction.CallbackContext _ = default)
    {
        if (selectedObject == null) return;

        foreach (var component in selectedObject.GetComponents<Component>())
        {
            string path = component.GetType().Name;

            if (expandedPaths.Contains(path))
            {
                expandedPaths.Remove(path);
            }
            else
            {
                expandedPaths.Add(path);
            }
        }
        RefreshDebugInfo();
    }


    private void AppendFoldout(StringBuilder stringBuilder, string path, Action content)
    {
        bool expanded = expandedPaths.Contains(path);

        stringBuilder.Append(expanded ? "[-] " : "[+] ");
        stringBuilder.AppendLine(path);

        if (expanded)
        {
            content.Invoke();
        }
    }

    private string GetFieldsInfo(object obj, int indentLevel, string parentPath)
    {
        if (obj == null) return Indent(indentLevel) + "null";

        if (visitedObjects.Contains(obj))
        {
            return Indent(indentLevel) + $"[Cyclic Reference: {obj.GetType().Name}]";
        }

        visitedObjects.Add(obj);

        var stringBuilder = new StringBuilder();
        Type type = obj.GetType();

        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            string fieldPath = $"{parentPath}.{field.Name}";
            object value = GetFieldValueSafe(field, obj);

            stringBuilder.Append(Indent(indentLevel));
            stringBuilder.Append($"{field.Name} ({field.FieldType.Name}): ");
            stringBuilder.AppendLine(GetValueDisplay(value, indentLevel, fieldPath));
        }

        return stringBuilder.ToString();
    }

    private object GetFieldValueSafe(FieldInfo field, object obj)
    {
        try
        {
            return field.GetValue(obj);
        }
        catch
        {
            return "unreadable";
        }
    }

    private string GetValueDisplay(object value, int indentLevel, string path)
    {
        if (value == null) return "null";

        if (value is string || IsSimple(value))
        {
            return value.ToString();
        }
        else if (value is UnityEngine.Object obj)
        {
            return obj != null ? obj.name : "null";
        }
        else if (value is IList list)
        {
            return GetListDisplay(list, indentLevel, path);
        }
        else
        {
            if (expandedPaths.Contains(path))
            {
                return "\n" + GetFieldsInfo(value, indentLevel + 1, path);
            }
            else
            {
                return "[+] (expand)";
            }
        }
    }

    private string GetListDisplay(IList list, int indentLevel, string path)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"List[{list.Count}]:");

        if (expandedPaths.Contains(path))
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                stringBuilder.Append(Indent(indentLevel + 1) + $"[{i}]: ");

                if (IsSimple(item))
                {
                    stringBuilder.AppendLine(item?.ToString() ?? "null");
                }
                else
                {
                    stringBuilder.AppendLine(GetFieldsInfo(item, indentLevel + 2, $"{path}[{i}]"));
                }
            }
        }
        else
        {
            stringBuilder.AppendLine(Indent(indentLevel + 1) + "[+] (expand with 'E')");
        }

        return stringBuilder.ToString();
    }

    private string Indent(int level)
    {
        return new string(' ', level * 2);
    }

    private bool IsSimple(object obj)
    {
        if (obj == null) return true;

        Type type = obj.GetType();
        return type.IsPrimitive
        || type.IsEnum
        || type == typeof(string)
        || type == typeof(Vector2)
        || type == typeof(Vector3)
        || type == typeof(Quaternion)
        || type == typeof(Color);
    }


    void OnEnable()
    {
        Input.Instance.GameInput.Debug.Click.started += SelectObject;
        Input.Instance.GameInput.Debug.E.started += ToggleComponents;
    }

    void OnDisable()
    {
        Input.Instance.GameInput.Debug.Click.started -= SelectObject;
        Input.Instance.GameInput.Debug.E.started -= ToggleComponents;
    }
}
#endif
