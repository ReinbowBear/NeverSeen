using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class Address
{
    public static async Task<GameObject> GetAssetByName(string address)
    {
        var handle = Addressables.InstantiateAsync(address);
        GameObject newObject = await handle.Task;

        if (newObject == null)
        {
            Debug.Log("Ассет: " + address + " не найден, проверьте имя!");
        }
        return newObject;
    }

    public static async Task<GameObject> GetAsset(AssetReference address)
    {
        var handle = Addressables.InstantiateAsync(address);
        GameObject newObject = await handle.Task;

        return newObject;
    }

    public static void DestroyAsset(GameObject asset)
    {
        asset.SetActive(false);
        Addressables.ReleaseInstance(asset);
    }
    
    //https://dtf.ru/u/306597-mihail-nikitin/273585-polza-addressables-v-unity3d-i-varianty-ispolzovaniya збилдить адресейбл надо по своему
}
