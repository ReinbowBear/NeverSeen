using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameSaver
{
    //public string filePath = Path.Combine(MyPaths.SAVE, "Save.json");


    public void SaveGame()
    {
        try
        {
            //Directory.CreateDirectory(MyPaths.SAVE);
            //File.WriteAllText(filePath, json);

            Debug.Log("💾 Сохранение");
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Ошибка при сохранении: " + ex);
        }
    }

    public void LoadGame()
    {
        //if (!File.Exists(filePath)) return;

        try
        {
            //string json = File.ReadAllText(filePath);
            //var loadedData = JsonConvert.DeserializeObject<GeneralData>(json);
            //generalData.LoadData(loadedData);
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка при загрузке: " + ex);
        }
    }


    public void DeleteSave()
    {
        //if (!File.Exists(filePath)) return;

        //File.Delete(filePath);
        Debug.Log("🗑️ Файл сохранения удалён");
    }
}
