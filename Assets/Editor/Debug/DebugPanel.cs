using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

//Что тебе нужно в Unity:
//UI Canvas с ScrollRect и Vertical Layout Group.
//
//Префаб CommandButtonPrefab:
//Button с Text внутри.
//
//Префаб InputFieldPrefab:
//Стандартный InputField с placeholder'ом Text.

public class DebugPanel : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelUI;                     // Вся панель
    public Transform commandContainer;             // Куда спавнятся кнопки
    public GameObject commandButtonPrefab;         // Префаб кнопки
    public GameObject inputFieldPrefab;            // Префаб поля ввода (если нужно)

    private Dictionary<string, MethodInfo> commands = new();
    private Dictionary<string, string> descriptions = new();
    private Dictionary<string, ParameterInfo[]> parameters = new();

    void Awake()
    {
        RegisterCommands();
        GenerateUI();
    }


    private void RegisterCommands()
    {
        commands.Clear();

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = method.GetCustomAttribute<DevCommandAttribute>();
                if (attribute != null)
                {
                    string key = attribute.Name.ToLower();

                    commands[key] = method;
                    descriptions[key] = attribute.Description ?? "";
                    parameters[key] = method.GetParameters();
                }
            }
        }
    }

    private void GenerateUI()
    {
        foreach (var pair in commands.OrderBy(p => p.Key))
        {
            string commandName = pair.Key;
            MethodInfo method = pair.Value;
            var paramInfo = parameters[commandName];

            var btnGO = Instantiate(commandButtonPrefab, commandContainer);
            var btnText = btnGO.GetComponentInChildren<Text>();
            var button = btnGO.GetComponent<Button>();

            btnText.text = $"{commandName} ({paramInfo.Length})";

            if (paramInfo.Length == 0)
            {
                button.onClick.AddListener(() =>
                {
                    method.Invoke(null, null);
                });
            }
            else
            {
                List<InputField> inputs = new();

                foreach (var parameter in paramInfo)
                {
                    var inputGO = Instantiate(inputFieldPrefab, btnGO.transform);
                    var input = inputGO.GetComponent<InputField>();

                    input.placeholder.GetComponent<Text>().text = parameter.Name + " (" + parameter.ParameterType.Name + ")";
                    inputs.Add(input);
                }

                button.onClick.AddListener(() =>
                {
                    object[] parsedArgs = new object[paramInfo.Length];
                    for (int i = 0; i < paramInfo.Length; i++)
                    {
                        parsedArgs[i] = ParseArg(inputs[i].text, paramInfo[i].ParameterType);
                    }

                    method.Invoke(null, parsedArgs);
                });
            }
        }
    }

    private object ParseArg(string input, Type type)
    {
        try
        {
            if (type == typeof(string)) return input ?? "";
            if (type == typeof(int)) return int.TryParse(input, out var i) ? i : 0;
            if (type == typeof(float)) return float.TryParse(input, out var f) ? f : 0f;
            if (type == typeof(bool)) return input.ToLower() == "true" || input == "1" || input == "on";

            if (type == typeof(Vector3))
            {
                var parts = input.Split(',');
                return new Vector3
                (
                    float.TryParse(parts.ElementAtOrDefault(0), out var x) ? x : 0f,
                    float.TryParse(parts.ElementAtOrDefault(1), out var y) ? y : 0f,
                    float.TryParse(parts.ElementAtOrDefault(2), out var z) ? z : 0f
                );
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[DebugCommand] Failed to parse '{input}' as {type.Name}: {ex.Message}");
        }

        Debug.LogWarning($"[DebugCommand] Unsupported or failed type: {type.Name}, input: '{input}'");
        return null;
    }
}


public static class DevCommands
{
    [DevCommand("kill", "Kill the player")]
    public static void Kill() => Debug.Log("Player died");

    [DevCommand("teleport", "Teleport to coords")]
    public static void Teleport(float x, float y, float z)
    {
        Debug.Log($"Teleport to {x}, {y}, {z}");
    }
}
