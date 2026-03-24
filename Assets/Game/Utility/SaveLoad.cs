using System.IO;
using UnityEngine;


public static class SaveLoad
{
    private static string savePath => Application.persistentDataPath;
    private static string assetsPath => Application.dataPath;
    private static string streamingAssets => Application.streamingAssetsPath;


    public static void Save<T>(T data, string fileName, ConfigType category)
    {
        var path = GetPath(category, fileName);
        CheckDirectory(path);

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static T Load<T>(string fileName, ConfigType category)
    {
        string path = GetPath(category, fileName);

        if (!File.Exists(path)) return default;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    public static void Delete(string fileName, ConfigType category)
    {
        var path = GetPath(category, fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }


    public static string GetPath(ConfigType category, string fileName = null)
    {
        var path = category switch
        {
            ConfigType.Save => Path.Combine(savePath, "Saves"),
            ConfigType.Input => Path.Combine(assetsPath, "Game/Configs/Inputs"),
            ConfigType.Graph => Path.Combine(assetsPath, "Game/Configs/Graphs"),
            ConfigType.Generated => Path.Combine(assetsPath, "Game/Configs/Generated"),
            ConfigType.Editor => Path.Combine(assetsPath, "Editor/Resources"),
            _ => savePath
        };

        return Path.Combine(path, fileName);
    }

    private static void CheckDirectory(string fullPath)
    {
        var directory = Path.GetDirectoryName(fullPath);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}

public enum ConfigType
{
    Save,
    Input,
    Graph,
    Generated,
    Editor
}
