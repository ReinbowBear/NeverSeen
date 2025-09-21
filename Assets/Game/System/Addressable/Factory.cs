using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Random = UnityEngine.Random;

public class Factory : MonoBehaviour
{
    private Dictionary<string, AsyncOperationHandle<GameObject>> handles = new();
    private DiContainer diContainer;

    [SerializeField] private string[] preloadKeys;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }

    async void Awake()
    {
        foreach (var key in preloadKeys)
        {
            await GetAsset(key);
        }
    }


    public async Task<GameObject> GetAsset(string key)
    {
        if (handles.ContainsKey(key)) return handles[key].Result;

        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Ошибка загрузки ассета имя: " + key);
            return null;
        }

        handles[key] = handle;
        return handle.Result;
    }


    public async Task<GameObject> Create(string key)
    {
        var prefab = await GetAsset(key);
        return diContainer.InstantiatePrefab(prefab);
    }

    public GameObject Instantiate(GameObject key)
    {
        return diContainer.InstantiatePrefab(key);
    }


    public object GetClassWithActivator(Type type, object[] args)
    {
        var myClass = Activator.CreateInstance(type, args);
        diContainer.Inject(myClass);
        return myClass;
    }

    public T GetClass<T>(params object[] args) where T : class
    {
        return diContainer.Instantiate<T>(args);
    }


    public async Task<string> GetRandomKey(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        if (locations == null || locations.Count == 0)
        {
            Debug.LogError("Нет предметов с меткой: " + label);
            return null;
        }

        var randomIndex = Random.Range(0, locations.Count);
        var item = locations[randomIndex];

        return item.PrimaryKey;
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


    void OnDestroy()
    {
        foreach (var handle in handles.Values)
        {
            Addressables.Release(handle);
        }
    }
}
