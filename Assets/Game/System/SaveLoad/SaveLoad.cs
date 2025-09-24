using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveLoad
{
    public string filePath = Path.Combine(MyPaths.SAVE, "Save.json");
    private GeneralData generalData;

    public SaveLoad(GeneralData generalData)
    {
        this.generalData = generalData;
    }


    private void CheckSave()
    {
        if (generalData.IsGameInit) return;
        
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

            var json = JsonConvert.SerializeObject(generalData);
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
            var loadedData = JsonConvert.DeserializeObject<GeneralData>(json);
            //generalData.LoadData(loadedData);

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
