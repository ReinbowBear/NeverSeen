using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Random = UnityEngine.Random;

public class Factory
{
    private Dictionary<string, AsyncOperationHandle<GameObject>> handles = new();
    private DiContainer diContainer;
    private World world;

    public Factory(DiContainer diContainer, World world)
    {
        this.diContainer = diContainer;
        this.world = world;
    }


    public async Task<GameObject> Create(string key)
    {
        var prefab = await GetAsset(key);
        return Instantiate(prefab); 
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

    public GameObject Instantiate(GameObject prefab)
    {
        var obj = diContainer.InstantiatePrefab(prefab);
        obj.transform.SetParent(null);

        world.AddEntity(obj);
        return obj;
    }


    public T GetClass<T>(params object[] args) where T : class
    {
        return diContainer.Instantiate<T>(args);
    }

    public void Inject(object injectable)
    {
        diContainer.Inject(injectable);
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
        if (!handles.TryGetValue(key, out var handle)) return;

        Addressables.Release(handle);
    }


    public void Clear()
    {
        foreach (var handle in handles.Values)
        {
            Addressables.Release(handle);
        }
    }
}
