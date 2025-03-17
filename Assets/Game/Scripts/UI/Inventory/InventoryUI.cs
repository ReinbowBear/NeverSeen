using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public Image[] abilitySlots;

    void Awake()
    {
        instance = this;
    }


    public void ShowItems(Inventory inventory)
    {
        for (byte i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.weapons[i] != null)
            {
                abilitySlots[i].sprite = inventory.weapons[i].UI.sprite;
            }
        }
    }
}
