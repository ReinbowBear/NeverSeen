using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Service]
public class Factory : IDisposable
{
    private Dictionary<string, AsyncOperationHandle> handles = new();
    private Dictionary<string, AsyncOperationHandle> labelHandles = new();

    public async Task<T> LoadAsync<T>(string address)
    {
        if (handles.TryGetValue(address, out var existingHandle)) return (T)existingHandle.Result;

        var handle = Addressables.LoadAssetAsync<T>(address);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load asset at {address}");
            return default;
        }

        handles[address] = handle;
        return handle.Result;
    }

    public async Task<IList<T>> LoadByLabelAsync<T>(string label)
    {
        if (labelHandles.TryGetValue(label, out var existingHandle)) return (IList<T>)existingHandle.Result;

        var handle = Addressables.LoadAssetsAsync<T>(label, null);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load assets with label {label}");
            return default;
        }

        labelHandles[label] = handle;
        return handle.Result;
    }


    public GameObject Instantiate(string address, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        if (!handles.ContainsKey(address) || handles[address].Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Asset {address} is not loaded");
            return null;
        }

        var asset = handles[address].Result;

        if (asset is GameObject obj)
        {
            return GameObject.Instantiate(obj, position, rotation, parent);
        }

        Debug.LogError($"Asset {address} is not a GameObject");
        return null;
    }

    public void Release(string address)
    {
        if (handles.ContainsKey(address))
        {
            Addressables.Release(handles[address]);
            handles.Remove(address);
        }
    }

    public void ReleaseLabel(string label)
    {
        if (labelHandles.TryGetValue(label, out var handle))
        {
            Addressables.Release(handle);
            labelHandles.Remove(label);
        }
    }


    public void Dispose()
    {
        foreach (var handle in handles.Values) Addressables.Release(handle);
        foreach (var handle in labelHandles.Values) Addressables.Release(handle);

        handles.Clear();
        labelHandles.Clear();
    }
}
