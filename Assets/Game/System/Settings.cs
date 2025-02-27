using System.IO;
using UnityEngine;

public static class Settings
{
    public static SettingsData settingsData = new SettingsData();

    public static void SaveSettings()
    {
        File.WriteAllText(GetFileName(), JsonUtility.ToJson(settingsData, true));
    }

    public static void LoadSettings()
    {
        string saveFile = File.ReadAllText(GetFileName());
        settingsData = JsonUtility.FromJson<SettingsData>(saveFile);
    }


    public static string GetFileName()
    {
        string saveFile = Application.persistentDataPath + "/settingsData" + ".save";
        return saveFile;
    }
}

[System.Serializable]
public struct SettingsData
{
    public bool isSoundOn;
}
