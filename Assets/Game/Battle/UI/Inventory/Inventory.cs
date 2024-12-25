using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AbilityDataBase gameAbilitys;
    [SerializeField] private GameObject itemPrefab;
    [Space]
    public ItemSlot[] weapons;
    public ItemSlot[] abilitys;
    public ItemSlot[] rings;
    public ItemSlot armor;


    public void AddItem(int index)
    {
        for (byte i = 0; i < abilitys.Length; i++)
        {
            if (abilitys[i].GetItem() == null)
            {
                Item newItem = Instantiate(itemPrefab, abilitys[i].transform).GetComponent<Item>();
                newItem.Init(gameAbilitys.containers[index]);

                break;
            }
        }
    }


    private void Save()
    {
        SaveInventory saveInventory = new SaveInventory();
        saveInventory.AbilityID = new int[abilitys.Length];
        
        for (byte i = 0; i < abilitys.Length; i++)
        {
            if (abilitys[i].GetItem() != null)
            {
                //saveInventory.AbilityID[i] = abilitys[i].GetItem().container.id;
            }
            else
            {
                saveInventory.AbilityID[i] = -1;
            }
        }

        SaveSystem.gameData.saveInventory = saveInventory; 
    }

    private void Load()
    {
        SaveInventory saveInventory = SaveSystem.gameData.saveInventory;

        for (byte i = 0; i < saveInventory.AbilityID.Length; i++)
        {
            if (saveInventory.AbilityID[i] < 0)
            {
                Item newItem = Instantiate(itemPrefab, abilitys[i].transform).GetComponent<Item>();
                newItem.Init(gameAbilitys.containers[saveInventory.AbilityID[i]]);
            }
        }
    }


    private void GetCharacter(MyEvent.OnCharacterInit CharacterInstantiate)
    {
        CharacterInstantiate.character.inventory = this;
    }

    void OnEnable()
    {
        EventBus.Add<MyEvent.OnCharacterInit>(GetCharacter);

        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnCharacterInit>(GetCharacter);

        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }
}

[System.Serializable]
public struct SaveInventory
{
    public int[] weaponID;
    public int[] AbilityID;
    public int[] ringID;
    public int armorID;
}
