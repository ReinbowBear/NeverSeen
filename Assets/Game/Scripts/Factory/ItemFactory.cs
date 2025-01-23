using System.Threading.Tasks;

public static class ItemFactory
{
    public static async Task<Item> GetItem(ItemSO itemContainer)
    {
        var itemObject = await Address.GetAssetByName("ItemPrefab");
        Item newItem = itemObject.GetComponent<Item>();

        newItem.Init(itemContainer);
        return newItem;
    }
}
