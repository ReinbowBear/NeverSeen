using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class AddressImporter
{
    private static string filePath = Path.Combine(MyPaths.SAVE, "AddressableAssets.json");
    public static Dictionary<string, List<string>> addressStorage;

    static AddressImporter()
    {
        ImportAddressable();
    }


    public static void ImportAddressable()
    {
        if (File.Exists(filePath))
        {
            addressStorage = new();

            string json = File.ReadAllText(filePath);
            addressStorage = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
        }
        else
        {
            Debug.Log("Addressable assets JSON file not found.");
        }
    }


    public static string GetRandomKey(string groupName)
    {
        if (!addressStorage.ContainsKey(groupName)) return null;

        var assetKeys = addressStorage[groupName];
        int randomIndex = Random.Range(0, assetKeys.Count);

        return assetKeys[randomIndex];
    }
}
