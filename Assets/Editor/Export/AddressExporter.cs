using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AddressExporter : IPreprocessBuildWithReport
{
    [MenuItem("Tools/Export Addressable Assets to JSON")]
    public static void ExportAddressable()
    {
        string filePath = Path.Combine(MyPaths.ADRESS, "AddressableAssets.json");
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        Dictionary<string, List<string>> addressData = new();

        foreach (var group in settings.groups)
        {
            List<string> assetKeys = new ();


            foreach (var entry in group.entries)
            {
                assetKeys.Add(entry.address);
            }

            addressData[group.Name] = assetKeys;
        }

        string json = JsonConvert.SerializeObject(addressData);

        Directory.CreateDirectory(MyPaths.ADRESS);
        File.WriteAllText(filePath, json);

        Debug.Log("Addressable assets exported to: " + MyPaths.ADRESS);
    }

    public int callbackOrder => 0;
    public void OnPreprocessBuild(BuildReport report)
    {
        ExportAddressable();
    }
}
