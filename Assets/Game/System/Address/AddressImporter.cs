using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class AddressImporter
{
    public static Dictionary<string, List<string>> addressStorage = new Dictionary<string, List<string>>();

    public static void ImportAddressable()
    {
        if (File.Exists(MyPaths.ADRESS + "/AddressableAssets.json"))
        {
            string json = File.ReadAllText(MyPaths.ADRESS + "/AddressableAssets.json");
            AddressGroupList groupList = JsonUtility.FromJson<AddressGroupList>(json);

            for (int i = 0; i < groupList.groups.Count; i++)
            {
                addressStorage[groupList.groups[i]] = groupList.keysList[i].keys;
            }
        }
        else
        {
            Debug.Log("Addressable assets JSON file not found.");
        }
    }


    public static string GetRandomKey(string groupName)
    {
        if (addressStorage.ContainsKey(groupName) == false)
        {
            return null;
        }

        var assetKeys = addressStorage[groupName];
        int randomIndex = Random.Range(0, assetKeys.Count);
        return assetKeys[randomIndex];
    }
}


[System.Serializable]
public class AddressGroupList
{
    public List<string> groups;
    public List<AddressAssetsList> keysList;

    public AddressGroupList(List<string> groups, List<AddressAssetsList> keysList)
    {
        this.groups = groups;
        this.keysList = keysList;
    }
}

[System.Serializable]
public class AddressAssetsList // словари не сереализуются, пришлось делать костыль
{
    public List<string> keys = new List<string>();

    public AddressAssetsList(List<string> keys)
    {
        this.keys = keys;
    }
}
