using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
// https://github.com/TeamSirenix/odin-serializer.git?path=UnityOdinSerializer оптимизированный и созданный специально для юнити, один инспектор сереализирует даже гейм обджекты (сохр на будущее, просто встаьв ссылку в пакет менеджер)
public static class SaveLoad // https://www.youtube.com/watch?v=1mf730eb5Wo&t=417s&ab_channel=SasquatchBStudios
{
    public static string filePath = Path.Combine(MyPaths.SAVE, "AddressableAssets.json");
    public static Dictionary<string, SaveData> saveDataDict { get; private set; } = new();

    #region saveLoad
    public static void SaveFile()
    {
        try
        {
            saveDataDict.Clear();

            EventBus.Invoke<OnSave>();

            var list = new List<SaveData>(saveDataDict.Values);
            var json = JsonConvert.SerializeObject(list);

            Directory.CreateDirectory(MyPaths.SAVE);
            File.WriteAllText(filePath, json);

            Debug.Log($"💾 Сохранение {list.Count} объектов");
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Ошибка при сохранении: " + ex);
        }
    }

    public static void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("⚠️ Файл сохранения не найден.");
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            var loadedData = JsonConvert.DeserializeObject<List<SaveData>>(json);

            var dict = new Dictionary<string, SaveData>();
            foreach (var data in loadedData)
            {
                dict[data.id] = data;
            }

            EventBus.Invoke<OnLoad>();
            //saveDataDict.Clear();
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Ошибка при загрузке: " + ex);
        }
    }


    public static void DeleteSave()
    {
        if (!File.Exists(filePath)) return;

        File.Delete(filePath);
        saveDataDict.Clear();

        #if UNITY_EDITOR
        Debug.Log("🗑️ Файл сохранения удалён");
        #endif
    }
    #endregion

    #region  reegister
    public static void Register(SaveData data)
    {
        saveDataDict[data.id] = data;
    }

    public static void UnRegister(SaveData data)
    {
        saveDataDict.Remove(data.id);
    }
    #endregion

    #region Dict func
    public static bool TryGetData(string id, out SaveData data)
    {
        return saveDataDict.TryGetValue(id, out data);
    }
    #endregion 
}
