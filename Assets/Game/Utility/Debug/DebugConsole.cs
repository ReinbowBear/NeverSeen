#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DebugConsole : MonoBehaviour
{
    [Header("UI")]
    public GameObject consoleUI;
    public InputField inputField;
    public Text outputText;
    public Text suggestionText;
    public int maxHistory;

    private Dictionary<string, MethodInfo> commands = new(); // все эти методы были статик, хз почему вдруг имеет смысл, сменил на private
    private Dictionary<string, string> descriptions = new();
    private List<string> history = new();
    private int historyIndex = -1;

    void Awake()
    {
        RegisterCommands();
    }


    #region keyboard keys
    private void Toggle(InputAction.CallbackContext _)
    {
        consoleUI.SetActive(!consoleUI.activeSelf);

        if (consoleUI.activeSelf)
        {
            inputField.ActivateInputField();

            Input.Instance.SetActiveInputs(false);

            Input.Instance.GameInput.Debug.BackSpace.started += Return;
            Input.Instance.GameInput.Debug.UpArrow.started += UpArrow;
            Input.Instance.GameInput.Debug.DownArrow.started += DownArrow;
            Input.Instance.GameInput.Debug.Tab.started += Tab;

            inputField.onValueChanged.AddListener(ShowSuggestions);
        }
        else
        {
            Input.Instance.GameInput.Debug.BackSpace.started -= Return;
            Input.Instance.GameInput.Debug.UpArrow.started -= UpArrow;
            Input.Instance.GameInput.Debug.DownArrow.started -= DownArrow;
            Input.Instance.GameInput.Debug.Tab.started -= Tab;

            Input.Instance.SetActiveInputs(true);

            inputField.onValueChanged.RemoveListener(ShowSuggestions);
        }
    }

    private void Return(InputAction.CallbackContext _)
    {
        string input = inputField.text;
        ExecuteCommand(input);

        history.Insert(0, input);
        if (history.Count > maxHistory)
        {
            history.RemoveAt(history.Count - 1);
        }
        historyIndex = -1;

        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void UpArrow(InputAction.CallbackContext _)
    {
        historyIndex = Mathf.Clamp(historyIndex + 1, 0, history.Count - 1);

        inputField.text = history[historyIndex];
        inputField.MoveTextEnd(false);
    }

    private void DownArrow(InputAction.CallbackContext _)
    {
        historyIndex = Mathf.Clamp(historyIndex - 1, -1, history.Count - 1);

        inputField.text = historyIndex >= 0 ? history[historyIndex] : "";
        inputField.MoveTextEnd(false);
    }

    private void Tab(InputAction.CallbackContext _)
    {
        AutoCompleteCommand();
        inputField.MoveTextEnd(false);
    }
    #endregion

    #region func
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
                    commands[attribute.Name.ToLower()] = method;
                    descriptions[attribute.Name.ToLower()] = attribute.Description ?? "";
                }
            }
        }

        commands["help"] = typeof(DebugConsole).GetMethod(nameof(Help), BindingFlags.NonPublic | BindingFlags.Instance);
        descriptions["help"] = "Shows all commands";
    }

    private void ExecuteCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        var parts = input.Split(' ');
        var commandName = parts[0].ToLower();
        var args = parts.Skip(1).ToArray();

        if (commands.TryGetValue(commandName, out var method))
        {
            var parameters = method.GetParameters();
            object[] parsedArgs = new object[parameters.Length];

            try
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    parsedArgs[i] = ParseArg(args.ElementAtOrDefault(i), parameters[i].ParameterType);
                }

                method.Invoke(null, parsedArgs);
                AppendOutput($"> {input}");
            }
            catch (Exception ex)
            {
                AppendOutput($"<color=red>Error: {ex.Message}</color>");
            }
        }
        else
        {
            AppendOutput($"<color=yellow>Unknown command: {commandName}</color>");
        }
    }

    private object ParseArg(string input, Type type)
    {
        try
        {
            if (type == typeof(string)) return input ?? "";
            if (type == typeof(int)) return int.Parse(input ?? "0");
            if (type == typeof(float)) return float.Parse(input ?? "0");
            if (type == typeof(bool)) return input == "true" || input == "1" || input == "on";
            if (type == typeof(Vector2))
            {
                var parts = (input ?? "0,0").Split(',');
                return new Vector2
                (
                    float.Parse(parts.ElementAtOrDefault(0)),
                    float.Parse(parts.ElementAtOrDefault(1))
                );
            }
            if (type == typeof(Vector3))
            {
                var parts = (input ?? "0,0,0").Split(',');
                return new Vector3
                (
                    float.Parse(parts.ElementAtOrDefault(0)),
                    float.Parse(parts.ElementAtOrDefault(1)),
                    float.Parse(parts.ElementAtOrDefault(2))
                );
            }
        }
        catch
        {
            throw new ArgumentException($"Failed to parse '{input}' as {type.Name}");
        }

        throw new ArgumentException($"Unsupported type: {type.Name}");
    }


    private void AppendOutput(string msg)
    {
        outputText.text += msg + "\n";
    }
    #endregion


    #region help
    private void ShowSuggestions(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) { suggestionText.text = ""; return; }

        string lower = input.ToLower();
        var match = commands.Keys.FirstOrDefault(k => k.StartsWith(lower));

        if (!string.IsNullOrEmpty(match))
        {
            suggestionText.text = match + " - " + descriptions[match];
        }
        else
        {
            suggestionText.text = "";
        }
    }

    private void AutoCompleteCommand()
    {
        string input = inputField.text.ToLower();
        if (string.IsNullOrWhiteSpace(input)) return;

        var match = commands.Keys.FirstOrDefault(k => k.StartsWith(input));
        if (!string.IsNullOrEmpty(match))
        {
            inputField.text = match + " ";
            ShowSuggestions(match);
        }
    }

    private void Help()
    {
        foreach (var pair in commands.OrderBy(p => p.Key))
        {
            AppendOutput($"<b>{pair.Key}</b> — {descriptions[pair.Key]}");
        }
    }

    private void Clear()
    {
        outputText.text = "";
    }
    #endregion

    #region other
    void Start()
    {
        Input.Instance.GameInput.Debug.Toggle.started += Toggle;

    }

    void OnDestroy()
    {
        Input.Instance.GameInput.Debug.Toggle.started -= Toggle;
    }
    #endregion
}


[AttributeUsage(AttributeTargets.Method)]
public class DevCommandAttribute : Attribute
{
    public string Name;
    public string Description;
    public DevCommandAttribute(string name, string description = "")
    {
        Name = name;
        Description = description;
    }
}


public static class DevCheats
{
    [DevCommand("give", "Gives an item by ID")]
    public static void GiveItem(string itemId)
    {
        Debug.Log($"[Cheat] Item given: {itemId}");
        // InventorySystem.Instance.Add(itemId);
    }

    [DevCommand("tp", "Teleports the player to x,y,z")]
    public static void Teleport(Vector3 pos)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = pos;
            Debug.Log($"[Cheat] Player moved to: {pos}");
        }
        else Debug.LogWarning("Player not found");
    }

    [DevCommand("setgod", "Toggles god mode (true/false)")]
    public static void SetGodMode(bool enabled)
    {
        Debug.Log($"[Cheat] God mode: {(enabled ? "ON" : "OFF")}");
        // PlayerStats.Instance.godMode = enabled;
    }

    [DevCommand("setmoney", "Sets player's money (int)")]
    public static void SetMoney(int amount)
    {
        Debug.Log($"[Cheat] Money set: {amount}");
        // PlayerStats.Instance.Money = amount;
    }

    [DevCommand("speed", "Set player move speed (float)")]
    public static void SetSpeed(float speed)
    {
        Debug.Log($"[Cheat] Speed set: {speed}");
        // PlayerMovement.Instance.SetSpeed(speed);
    }

    [DevCommand("move2d", "Move in 2D space (x,y)")]
    public static void Move2D(Vector2 target)
    {
        Debug.Log($"[Cheat] Move to 2D point: {target}");
        // player.transform.position = new Vector3(target.x, 0, target.y);
    }
}
#endif
