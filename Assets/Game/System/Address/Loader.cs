using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class Loader
{
    private static readonly Dictionary<string, AsyncOperationHandle> handleAssets = new();
    private static readonly Dictionary<string, short> refCounts = new();

    private static readonly Dictionary<string, Task<AsyncOperationHandle>> loadingTasks = new();


    public static async Task<T> LoadAssetAsync<T>(string key) where T : UnityEngine.Object
    {
        if (handleAssets.ContainsKey(key))
        {
            refCounts[key]++;
            return (T)handleAssets[key].Result;
        }


        if (loadingTasks.TryGetValue(key, out var existingTask)) // решение необычной проблемы, двойной загрузки в 1 момент, когда первая провека не проходит
        {
            var pastHandle = await existingTask;
            refCounts[key]++;
            return (T)pastHandle.Result;
        }

        var newTask = new TaskCompletionSource<AsyncOperationHandle>();
        loadingTasks[key] = newTask.Task;


        var handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Ошибка загрузки ассета имя: " + key);
        }

        handleAssets[key] = handle;
        refCounts[key] = 1;

        return handle.Result;
    }


    public static void Release(string key)
    {
        if (!handleAssets.ContainsKey(key))
        {
            return;
        }

        refCounts[key]--;

        if (refCounts[key] <= 0)
        {
            Addressables.Release(handleAssets[key]);

            handleAssets.Remove(key);
            refCounts.Remove(key);
        }
    }

    public static void ReleaseAllAssets()
    {
        foreach (var handle in handleAssets.Values)
        {
            Addressables.Release(handle);
        }

        handleAssets.Clear();
        refCounts.Clear();
    }
}
