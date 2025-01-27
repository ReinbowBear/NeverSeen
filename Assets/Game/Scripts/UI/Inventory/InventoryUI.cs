using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] abilitySlots;
    public ItemSlot[] ringSlots;
    public ItemSlot armorSlot;
    private SaveInventory saveInventory;

    private Entity myCharacter;

    private async void ShowItems()
    {
        for (byte i = 0; i < myCharacter.inventory.abilitys.Length; i++)
        {
            if (myCharacter.inventory.abilitys[i] != null)
            {
                Item newItem = await ItemFactory.GetItem(myCharacter.inventory.abilitys[i].stats);
                newItem.transform.SetParent(abilitySlots[i].transform, false);
                newItem.Init(myCharacter.inventory.abilitys[i].stats);
            }
        }
    }


    private void Save()
    {
        saveInventory = new SaveInventory();
        saveInventory.abilitys = new string[abilitySlots.Length];
        saveInventory.rings = new string[abilitySlots.Length];
        
        for (byte i = 0; i < abilitySlots.Length; i++)
        {
            if (abilitySlots[i].GetItem() != null)
            {
                saveInventory.abilitys[i] = abilitySlots[i].GetItem().itemSO.UI.itemName;
            }
        }

        for (byte i = 0; i < ringSlots.Length; i++)
        {
            if (ringSlots[i].GetItem() != null)
            {
                saveInventory.rings[i] = abilitySlots[i].GetItem().itemSO.UI.itemName;
            }
        }

        if (armorSlot.GetItem() != null)
        {
            saveInventory.armor = armorSlot.GetItem().itemSO.UI.itemName;
        }

        SaveSystem.gameData.saveInventory = saveInventory; 
    }

    private void Load()
    {
        saveInventory = SaveSystem.gameData.saveInventory;
    }


    private void GetCharacter(MyEvent.OnEntityInit CharacterInstantiate)
    {
        if (CharacterInstantiate.entity.currentStats.isPlayer == true)
        {
            myCharacter = CharacterInstantiate.entity;
            ShowItems();
        }
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntityInit>(GetCharacter);

        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntityInit>(GetCharacter);

        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }
}


[System.Serializable]
public struct SaveInventory
{
    public string[] abilitys;
    public string[] rings;
    public string armor;
}
