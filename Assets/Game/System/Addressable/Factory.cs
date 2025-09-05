using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class Factory : IDisposable
{
    private Dictionary<string, AsyncOperationHandle<GameObject>> handles = new();
    private DiContainer diContainer;

    public Factory(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }


    public async Task Register(string key)
    {
        if (handles.ContainsKey(key)) return;

        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Ошибка загрузки ассета имя: " + key);
            return;
        }

        handles.Add(key, handle);
    }


    public T CreateClass<T>() where T : class
    {
        return diContainer.Instantiate<T>();
    }

    public GameObject Create(string key)
    {
        if (!handles.TryGetValue(key, out var handle))
        {
            Debug.LogError($"[{nameof(Factory)}] Объект: [{key}] не зарегестрирован!");
            return null;
        }

        return diContainer.InstantiatePrefab(handle.Result);
    }


    public void Release(string key)
    {
        if (!handles.TryGetValue(key, out var handle))
        {
            Debug.LogError($"[{nameof(Factory)}] Объект: {key} не зарегестрирован!");
            return;
        }

        Addressables.Release(handle);
    }


    public void Dispose()
    {
        foreach (var handle in handles.Values)
        {
            Addressables.Release(handle);
        }
    }
}
