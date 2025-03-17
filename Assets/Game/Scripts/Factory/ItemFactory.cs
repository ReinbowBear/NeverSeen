using System.Threading.Tasks;

public static class ItemFactory
{
    public static async Task<ItemUI> GetItem(Item itemData)
    {
        var itemObject = await Address.GetAssetByName("ItemPrefab");
        ItemUI newItem = itemObject.GetComponent<ItemUI>();

        newItem.Init(itemData);
        return newItem;
    }
}
