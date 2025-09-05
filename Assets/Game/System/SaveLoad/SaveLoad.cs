using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveLoad // https://github.com/TeamSirenix/odin-serializer.git?path=UnityOdinSerializer один инспектор
{
    public string filePath = Path.Combine(MyPaths.SAVE, "Save.json");
    private GameData gameData;

    public SaveLoad(GameData gameData)
    {
        this.gameData = gameData;
    }


    private void CheckSave()
    {
        if (gameData.General.IsGameInit) return;
        
        if (File.Exists(filePath))
        {
            LoadGame();
        }
        else
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        try
        {
            EventBus.Invoke<OnSave>();

            var json = JsonConvert.SerializeObject(gameData);
            Directory.CreateDirectory(MyPaths.SAVE);
            File.WriteAllText(filePath, json);

            Debug.Log("💾 Сохранение");
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Ошибка при сохранении: " + ex);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Файл сохранения не найден!");
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            var loadedData = JsonConvert.DeserializeObject<GameData>(json);
            gameData.LoadData(loadedData);

            EventBus.Invoke<OnLoad>();
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка при загрузке: " + ex);
        }
    }


    public void DeleteSave()
    {
        if (!File.Exists(filePath)) return;

        File.Delete(filePath);
        Debug.Log("🗑️ Файл сохранения удалён");
    }
}
