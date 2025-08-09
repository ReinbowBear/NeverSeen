using System.Collections.Generic;
using UnityEngine;

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
            var handle = Loader.LoadAssetAsync<GameObject>(building);
            await handle;
        }
    }
}
