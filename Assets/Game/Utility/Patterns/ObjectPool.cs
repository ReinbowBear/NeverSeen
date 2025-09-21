using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPool : IDisposable
{
    private string addressKey;
    private Transform root;

    private Queue<GameObject> freeObjects = new();
    private AsyncOperationHandle<GameObject> handle;

    private GameObject prefab => handle.IsDone ? handle.Result : null;
    private bool isLoaded => handle.IsDone;
    private bool dontDestroy;

    public ObjectPool(string addressKey, bool dontDestroy = false)
    {
        this.addressKey = addressKey;
        this.dontDestroy = dontDestroy;
    }


    public async Task LoadAsync()
    {
        if (handle.IsValid()) return;

        handle = Addressables.LoadAssetAsync<GameObject>(addressKey);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"ошибка загрузки префаба: {addressKey}");
            return;
        }

        var poolObj = UnityEngine.Object.Instantiate(new GameObject());
        root = poolObj.transform;

        if (!dontDestroy)
        {
            var cleater = poolObj.AddComponent<ObjectPoolCleaner>();
            cleater.Init(this);
        }
        else
        {
            UnityEngine.Object.DontDestroyOnLoad(poolObj);
        }

        poolObj.name = addressKey + "_Pool";
    }


    public GameObject Get()
    {
        if (!isLoaded) return null;

        GameObject obj;
        if (freeObjects.Count > 0)
        {
            obj = freeObjects.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = UnityEngine.Object.Instantiate(handle.Result, root);
        }

        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        freeObjects.Enqueue(obj);
    }


    public void Dispose()
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }

        if (!dontDestroy) // а если он дестрой то объект удалит ObjectPoolCleaner
        {
            UnityEngine.Object.Destroy(root.gameObject);
        }
    }
}

public class ObjectPoolCleaner : MonoBehaviour
{
    private ObjectPool objectPool;

    public void Init(ObjectPool objectPool)
    {
        this.objectPool = objectPool;
    }

    void OnDestroy()
    {
        objectPool.Dispose();
    }
}
