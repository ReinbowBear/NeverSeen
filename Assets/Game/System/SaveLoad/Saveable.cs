using System;
using Newtonsoft.Json;
using UnityEngine;

public abstract class Saveable : MonoBehaviour
{
    [SerializeField, ReadOnly] protected string UniqueID { get; private set; }

    protected void SaveData<T>(T data) where T : struct
    {
        if (string.IsNullOrEmpty(UniqueID))
        {
            UniqueID = Guid.NewGuid().ToString();
        }

        var wrapper = new SaveData
        {
            id = UniqueID,
            json = JsonConvert.SerializeObject(data)
        };

        SaveLoad.Register(wrapper);
    }

    protected T? LoadData<T>() where T : struct
    {
        if (!SaveLoad.TryGetData(UniqueID, out var data)) return null;

        try
        {
            return JsonConvert.DeserializeObject<T>(data.json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Ошибка десериализации ID: {UniqueID}, Тип: {typeof(T)}\n{ex}");
            return null;
        }
    }


    protected abstract void OnSave(OnSave _); // передай нужную структуру на сохранение в метод SaveData
    protected abstract void OnLoad(OnLoad _); // загрузи свою структуру данных в методе LoadData (грузит по айди но нужно приведение по типу)

    protected virtual void OnEnable()
    {
        EventBus.AddSubscriber<OnSave>(OnSave);
        EventBus.AddSubscriber<OnLoad>(OnLoad);
    }

    protected virtual void OnDisable()
    {
        EventBus.RemoveSubscriber<OnSave>(OnSave);
        EventBus.RemoveSubscriber<OnLoad>(OnLoad);
    }
}


[System.Serializable]
public struct SaveData
{
    public string id;
    public string json;
}
