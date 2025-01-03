using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] abilitySlots;
    public ItemSlot[] ringSlots;
    public ItemSlot armorSlot;
    private SaveInventory saveInventory;

    private ItemFactory itemFactory;
    private Entity myCharacter;

    private async void ShowItems()
    {
        for (byte i = 0; i < myCharacter.inventory.abilitys.Length; i++)
        {
            if (myCharacter.inventory.abilitys[i] != null)
            {
                Item newItem = await itemFactory.GetItem(myCharacter.inventory.abilitys[i]);
                abilitySlots[i].transform.SetParent(newItem.transform, false);
            }
        }
    }


    private void Save()
    {
        saveInventory = new SaveInventory();
        saveInventory.abilitysID = new int[abilitySlots.Length];
        saveInventory.ringsID = new int[abilitySlots.Length];
        
        for (byte i = 0; i < abilitySlots.Length; i++)
        {
            if (abilitySlots[i].GetItem() != null)
            {
                saveInventory.abilitysID[i] = abilitySlots[i].GetItem().container.containerID;
            }
            else
            {
                saveInventory.abilitysID[i] = -1;
            }
        }

        for (byte i = 0; i < ringSlots.Length; i++)
        {
            if (ringSlots[i].GetItem() != null)
            {
                saveInventory.ringsID[i] = abilitySlots[i].GetItem().container.containerID;
            }
            else
            {
                saveInventory.ringsID[i] = -1;
            }
        }

        if (armorSlot.GetItem() != null)
        {
            saveInventory.armorID = armorSlot.GetItem().container.containerID;
        }
        else
        {
            saveInventory.armorID = -1;
        }

        SaveSystem.gameData.saveInventory = saveInventory; 
    }

    private void Load()
    {
        saveInventory = SaveSystem.gameData.saveInventory;
    }


    private void GetCharacter(MyEvent.OnEntityInit CharacterInstantiate)
    {
        myCharacter = CharacterInstantiate.entity;
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
    public int[] abilitysID;
    public int[] ringsID;
    public int armorID;
}
