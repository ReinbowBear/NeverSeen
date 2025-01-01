using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private ItemDatabase gameItems;
    [SerializeField] private AssetReference itemPrefab;
    [Space]
    [SerializeField] private Inventory inventory;

    public async Task<Item> GetItem(ItemContainer itemContainer)
    {
        var itemObject = await Content.GetAsset(itemPrefab);
        Item newItem = itemObject.GetComponent<Item>();

        newItem.Init(itemContainer);
        return newItem;
    }


    private async void StartedItems(MyEvent.OnCharacterInit CharacterInstantiate)
    {
        Debug.Log("создание стартовых предметов (возможна перезапись сохранением?)");

        for (byte i = 0; i < CharacterInstantiate.character.stats.abilitys.Length; i++)
        {
            ItemContainer itemContainer = gameItems.GetItemByName(CharacterInstantiate.character.stats.abilitys[i]);

            if (itemContainer != null)
            {
                Item newItem = await GetItem(itemContainer);
                newItem.transform.SetParent(inventory.abilitySlots[i].transform, false);
            }
            else
            {
                Debug.Log("такого предмета нету для создания!");
            }
        }
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnCharacterInit>(StartedItems, 1);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnCharacterInit>(StartedItems);
    }
}
