using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ObjectPool
{
    private static readonly Dictionary<string, Pool> pools = new();
    private static readonly Dictionary<GameObject, string> objectToKey = new();

    private class Pool
    {
        public AsyncOperationHandle<GameObject> Handle;
        public GameObject Prefab => Handle.Result;
        public Queue<GameObject> FreeObjects = new();
    }

    static ObjectPool()
    {
        EventBus.Subscribe<OnSceneRelease>(Clear);
    }


    #region Register
    public static async Task Register(string key)
    {
        if (pools.ContainsKey(key)) return;

        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Ошибка загрузки ассета имя: " + key);
            return;
        }

        pools[key] = new Pool { Handle = handle };
    }
    #endregion

    #region Get and Return
    public static GameObject Get(string key)
    {
        if (!pools.TryGetValue(key, out var pool))
        {
            Debug.LogError($"[{nameof(ObjectPool)}] Объект: {key} не зарегестрирован!");
            return null;
        }

        GameObject objectToGet;
        if (pool.FreeObjects.Count > 0)
        {
            objectToGet = pool.FreeObjects.Dequeue();
            objectToGet.SetActive(true);
        }
        else
        {
            objectToGet = Object.Instantiate(pool.Prefab);
        }

        objectToKey[objectToGet] = key;
        return objectToGet;
    }

    public static void Return(GameObject obj)
    {
        if (!objectToKey.TryGetValue(obj, out var key))
        {
            Debug.LogError($"[{nameof(ObjectPool)}] Объект: {obj} не был получен через {nameof(ObjectPool)}!");
            return;
        }

        obj.SetActive(false);
        pools[key].FreeObjects.Enqueue(obj);
    }
    #endregion

    #region Release and Clear
    public static void Release(string key)
    {
        if (!pools.TryGetValue(key, out var pool)) return;

        foreach (var obj in pool.FreeObjects)
        {
            objectToKey.Remove(obj);
        }

        Addressables.Release(pool.Handle);
        pool.FreeObjects.Clear();
        pools.Remove(key);
    }

    public static void Clear(OnSceneRelease _ = null)
    {
        foreach (var pool in pools.Values)
        {
            Addressables.Release(pool.Handle);
            pool.FreeObjects.Clear();
        }

        pools.Clear();
        objectToKey.Clear();
    }
    #endregion
}
