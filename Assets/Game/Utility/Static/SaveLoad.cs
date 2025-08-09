using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class SaveLoad // https://www.youtube.com/watch?v=1mf730eb5Wo&t=417s&ab_channel=SasquatchBStudios
{
    public static GameData gameData = new(); // потенциальные проблемы, смена имени класса не позволяет загрузить сохранение (там ещё старое имя), а ещё гейм дата не загружается щас
    private static Dictionary<string, List<Saveable>> saveables = new();
    public static readonly string filePath = MyPaths.SAVE + "/GameData.json";

    public static void SaveFile()
    {
        try
        {
            var json = JsonConvert.SerializeObject(saveables);
            File.WriteAllText(filePath, json);
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
            Debug.Log("⚠️ Файл сохранения не найден!");
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            var loadedDictionary = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(json);

            saveables.Clear();

            foreach (var keyValue in loadedDictionary)
            {
                var typeName = keyValue.Key;
                var array = keyValue.Value;

                var type = Type.GetType(typeName);
                if (type == null || !typeof(Saveable).IsAssignableFrom(type)) continue;

                var list = (List<Saveable>)array.ToObject(typeof(List<>).MakeGenericType(type));
                saveables[typeName] = list;
            }

            #if UNITY_EDITOR
            Debug.Log("✅ Загружено:\n" + json);
            #endif
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Ошибка при загрузке: " + ex);
        }
    }

    #region  reegister
    public static void Register<T>(T item) where T : Saveable
    {
        string key = typeof(T).FullName;

        if (!saveables.ContainsKey(key))
        {
            saveables[key] = new List<Saveable>();
        }
        saveables[key].Add(item);
    }

    public static void UnRegister<T>(T item) where T : Saveable
    {
        string key = typeof(T).FullName;

        if (saveables.TryGetValue(key, out var list))
        {
            list.Remove(item);

            if (list.Count == 0)
            {
                saveables.Remove(key);
            }
        }
    }
    #endregion

    #region GetData
    public static T Get<T>() where T : Saveable, new()
    {
        string key = typeof(T).FullName;

        if (saveables.TryGetValue(key, out var list))
        {
            return list.FirstOrDefault() as T ?? new T();
        }

        return new T();
    }

    public static IEnumerable<T> GetAll<T>() where T : Saveable
    {
        string key = typeof(T).FullName;

        if (saveables.TryGetValue(key, out var list))
        {
            return list.Cast<T>();
        }

        return Enumerable.Empty<T>();
    }
    #endregion

    #region other
    public static void DeleteSave()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);

            #if UNITY_EDITOR
            Debug.Log("🗑️ Файл сохранения удалён.");
            #endif
        }

        saveables.Clear();
    }
    #endregion
}

[System.Serializable]
public class GameData
{
    public byte sceneIndex;
    public int seed;
}


public abstract class Saveable
{
    public Saveable()
    {
        SaveLoad.Register(this);
    }

    public void RemoveSaveable()
    {
        SaveLoad.UnRegister(this);
    }
}