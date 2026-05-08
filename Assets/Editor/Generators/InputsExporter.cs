using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
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


        SaveLoad.Save(json, "DefaultInputs.json", ConfigType.Input);
        Debug.Log("Inputs Map exported");
    }

    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        ExportInputs();
    }
}
