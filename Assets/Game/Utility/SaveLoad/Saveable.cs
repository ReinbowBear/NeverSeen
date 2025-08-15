using System;
using Newtonsoft.Json;
using UnityEngine;

public abstract class Saveable<TsaveData> : MonoBehaviour where TsaveData : SaveData, new()
{
    [SerializeField, HideInInspector]
    protected string uniqueId;
    public string UniqueID => uniqueId;

    protected virtual void Awake()
    {
        if (!string.IsNullOrEmpty(uniqueId)) return;

        uniqueId = Guid.NewGuid().ToString();
    }


    protected abstract TsaveData GetDataClass(); // просто верни для функции ниже свой сереализуемый класс
    protected virtual void SaveData(OnSave _)
    {
        TsaveData saveData = GetDataClass();

        var wrapper = new SaveData
        {
            id = UniqueID,
            type = GetType().FullName,
            json = JsonConvert.SerializeObject(saveData)
        };

        SaveLoad.Register(saveData);
    }

    protected virtual void LoadData(OnLoad onLoad)
    {
        if (!onLoad.TryGetDataById(UniqueID, out var data)) return;

        var saveData = JsonConvert.DeserializeObject(data.json, Type.GetType(data.type)) as TsaveData;
        if (saveData == null)
        {
            Debug.LogWarning($"ошибка десереализации! ID объекта: {UniqueID} типа: {typeof(Saveable<TsaveData>)}");
            return;
        }

        ApplyLoadData(saveData);
    }
    protected abstract void ApplyLoadData(TsaveData data); // твоё сохранение в качесстве аргумента функции, просто присвой себе в переменную


    protected virtual void OnEnable()
    {
        EventBus.Subscribe<OnSave>(SaveData);
        EventBus.Subscribe<OnLoad>(LoadData);
    }

    protected virtual void OnDisable()
    {
        EventBus.Subscribe<OnSave>(SaveData);
        EventBus.Subscribe<OnLoad>(LoadData);
    }
}


[System.Serializable]
public class SaveData
{
    public string id;
    public string type;
    public string json;
}
