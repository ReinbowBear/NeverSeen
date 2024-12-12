using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private ItemSlot[] slots;


    public void AddItem(ItemSO itemSO)
    {
        Item newItem = Instantiate(itemSlotPrefab, transform).GetComponent<Item>();

        for (byte i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(newItem);

                newItem.itemSO = itemSO;
                newItem.RenderItem();
                break;
            }
        }

        Debug.Log("создание предметов не использует адресейбл");
    }


    private void Save()
    {
        SaveInventory saveInventory = new SaveInventory();

        saveInventory.itemsSO = new ItemSO[slots.Length];

        for (byte i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveInventory.itemsSO[i] = slots[i].item.itemSO;
            }
        }

        SaveSystem.gameData.saveInventory = saveInventory; 
    }

    private void Load()
    {
        SaveInventory saveInventory = SaveSystem.gameData.saveInventory;

        for (byte i = 0; i < slots.Length; i++)
        {
            if (saveInventory.itemsSO[i] != null)
            {
                AddItem(saveInventory.itemsSO[i]);
            }
        }
    }


    void OnEnable()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDisable()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }
}

[System.Serializable]
public struct SaveInventory
{
    public ItemSO[] itemsSO; //стоит переделать на индекс позже, так как классы не сохраняются
}
