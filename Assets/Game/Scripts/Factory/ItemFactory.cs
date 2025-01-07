using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private AssetReference itemPrefab;

    public async Task<Item> GetItem(ItemContainer itemContainer)
    {
        var itemObject = await Address.GetAsset(itemPrefab);
        Item newItem = itemObject.GetComponent<Item>();

        newItem.Init(itemContainer);
        return newItem;
    }
}
