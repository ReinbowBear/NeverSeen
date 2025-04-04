using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class ItemFactory
{
    public static async Task<ItemUI> GetItem(Item itemData)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("ItemPrefab");
        await handle.Task;

        var release = handle.Result.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;

        ItemUI newItem = handle.Result.GetComponent<ItemUI>();
        newItem.Init(itemData);
        
        return newItem;
    }
}
