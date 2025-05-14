using System;
using System.IO;
using UnityEngine;

public static class SaveSystem // https://www.youtube.com/watch?v=1mf730eb5Wo&t=417s&ab_channel=SasquatchBStudios
{    
    public static GameData gameData = new GameData();
    public static readonly string filePath = "Assets/Game/Save/GameData.save";

    public static void SaveFile()
    {
        try 
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(gameData, true));
        } 
        catch (Exception excep) 
        {
            Debug.Log("игра не сохранена! " + excep);
        }
    }

    public static void LoadFile()
    {
        string saveFile = File.ReadAllText(filePath);
        gameData = JsonUtility.FromJson<GameData>(saveFile);
    }


    public static void DeleteSave()
    {        
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}


[System.Serializable]
public struct GeneralData
{
    public string character;
    public byte sceneIndex;
    public int seed;
}

[System.Serializable]
public class GameData
{
    public GeneralData generalData = new GeneralData();
}