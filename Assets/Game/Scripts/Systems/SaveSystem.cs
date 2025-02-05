using System;
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
        string saveContent = File.ReadAllText(GetFileName());
        gameData = JsonUtility.FromJson<GameData>(saveContent);
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
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }
}


[System.Serializable] //сериализация позволяет записывать данные в файл
public class GameData
{
    public GeneralData generalData = new GeneralData();

    public SaveDangeonMap saveDangeonMap;
    public SaveLoot saveLoot;
}

[System.Serializable]
public struct GeneralData
{
    public byte characteIndex;
    public byte sceneIndex;
    public int seed;
}