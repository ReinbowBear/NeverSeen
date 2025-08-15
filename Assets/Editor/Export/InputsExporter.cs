using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsExporter : IPreprocessBuildWithReport
{
    [MenuItem("Tools/Export Inputs Map to JSON")]
    public static void ExportInputs()
    {
        InputActionAsset inputAsset = new GameInput().asset;

        if (inputAsset == null)
        {
            Debug.LogError("InputActionAsset не найден");
            return;
        }

        string json = inputAsset.SaveBindingOverridesAsJson();
        string filePath = Path.Combine(MyPaths.INPUTS, "DefaultInputs.json");

        Directory.CreateDirectory(MyPaths.INPUTS);

        File.WriteAllText(filePath, json);
        Debug.Log("Inputs Map exported to: " + MyPaths.INPUTS);
    }
    
    public int callbackOrder => 0;
    public void OnPreprocessBuild(BuildReport report)
    {
        ExportInputs();
    }
}
