using System.IO;
using UnityEngine;

public static class SaveSystem //https://www.youtube.com/watch?v=1mf730eb5Wo&t=417s&ab_channel=SasquatchBStudios
{    
    public static GameData gameData = new GameData();

    public static void SaveFile()
    {
        File.WriteAllText(GetFileName(), JsonUtility.ToJson(gameData, true));
    }

    public static void LoadFile()
    {
        string saveFile = File.ReadAllText(GetFileName());
        gameData = JsonUtility.FromJson<GameData>(saveFile);
    }


    public static void DeleteSave()
    {        
        if (File.Exists(GetFileName()))
        {
            File.Delete(GetFileName());
        }
    }


    public static string GetFileName()
    {
        string saveFile = "Assets/Game/Save/gameData.save";
        return saveFile;
    }
}


[System.Serializable]
public class GameData
{
    public GeneralData generalData = new GeneralData();
}

[System.Serializable]
public struct GeneralData
{
    public string character;
    public byte sceneIndex;
    public int seed;
}