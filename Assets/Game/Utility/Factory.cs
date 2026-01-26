using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Factory : IDisposable
{
    private Dictionary<string, AsyncOperationHandle<UnityEngine.Object>> handles = new ();

    public async Task<UnityEngine.Object> LoadAsync(string address)
    {
        if (handles.ContainsKey(address)) return handles[address].Result;

        var handle = Addressables.LoadAssetAsync<UnityEngine.Object>(address);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load asset at {address}");
            return null;
        }

        handles[address] = handle;
        return handle.Result;
    }

    public UnityEngine.Object Instantiate(string address, Vector3 position = default, Quaternion rotation = default)
    {
        if (!handles.ContainsKey(address) || handles[address].Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Asset {address} is not loaded");
            return null;
        }

        var asset = handles[address].Result;

        if (asset is GameObject obj)
        {
            return UnityEngine.Object.Instantiate(obj, position, rotation);
        }

        return asset;
    }

    public void Release(string address)
    {
        if (handles.ContainsKey(address))
        {
            Addressables.Release(handles[address]);
            handles.Remove(address);
        }
    }


    public void Dispose()
    {
        foreach (var handle in handles.Values)
        {
            Addressables.Release(handle);
        }
    }
}
