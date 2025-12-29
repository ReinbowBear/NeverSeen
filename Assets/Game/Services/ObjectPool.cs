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

    private Queue<GameObject> freeObjects;
    private AsyncOperationHandle<GameObject> handle;
    private Factory factory;

    public ObjectPool(Factory factory)
    {
        this.factory = factory;
    }


    public void Init(string newAddressKey, Transform newRoot = null)
    {
        addressKey = newAddressKey;
        freeObjects = new();

        if (newRoot != null) return;

        var poolObj = UnityEngine.Object.Instantiate(new GameObject());
        root = poolObj.transform;
        poolObj.name = newAddressKey + "_Root";
    }


    public async Task<GameObject> Get()
    {
        GameObject obj;
        if (freeObjects.Count > 0)
        {
            obj = freeObjects.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            await factory.LoadAsync(addressKey);
            obj = GameObject.Instantiate(handle.Result, root);
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
    }
}
