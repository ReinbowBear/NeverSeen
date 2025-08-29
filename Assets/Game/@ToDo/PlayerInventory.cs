using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerInventory : MonoBehaviour
{
    public List<string> buildings;
    public List<string> items;

    void Awake()
    {
        LoadAssets();
    }


    public async void LoadAssets()
    {
        foreach (var building in buildings)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(building);
            await handle.Task;
        }
    }

    void OnDestroy()
    {
        foreach (var key in buildings)
        {
            Addressables.Release(key);
        }
    }
}
