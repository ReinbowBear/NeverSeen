using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputsExporter
{
    [MenuItem("Tools/Export Inputs Map to JSON")]
    private static void ExportInputs()
    {
        InputActionAsset inputAsset = new GameInput().asset;
        
        if (inputAsset == null)
        {
            Debug.LogError("InputActionAsset не найден");
            return;
        }

        string json = inputAsset.SaveBindingOverridesAsJson();
        string filePath = MyPaths.INPUTS + "/DefaultInputs.json";

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        File.WriteAllText(filePath, json);
        Debug.Log("Inputs Map exported to: " + filePath);
    }
}
