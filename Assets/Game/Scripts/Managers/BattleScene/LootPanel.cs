using UnityEngine;

public class LootPanel : MonoBehaviour
{
    public static LootPanel instance;

    [SerializeField] private ItemSlot[] LootInventory;

    void Awake()
    {
        instance = this;
    }

    public void AddLoot()
    {
        
    }


    public void ShowLoot()
    {
        for (byte i = 0; i < LootInventory.Length; i++)
        {
            
        }
    }
}
