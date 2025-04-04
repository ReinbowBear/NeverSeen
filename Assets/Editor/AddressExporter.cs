using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public static class AddressExporter
{
    [MenuItem("Tools/Export Addressable Assets to JSON")]
    private static void ExportAddressable()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        List<string> groupNames = new List<string>();
        List<AddressAssetsList> keys = new List<AddressAssetsList>();

        for (int i = 0; i < settings.groups.Count; i++)
        {
            List<string> assetKeys = new List<string>();

            foreach (var entry in settings.groups[i].entries)
            {
                assetKeys.Add(entry.address);
            }

            groupNames.Add(settings.groups[i].Name);
            keys.Add(new AddressAssetsList(assetKeys));
        }

        string json = JsonUtility.ToJson(new AddressGroupList(groupNames, keys), true);
        string filePath = "Assets/Game/Save/AddressableAssets.json";
        File.WriteAllText(filePath, json);

        Debug.Log("Addressable assets exported to: " + filePath);
    }
}
