using UnityEngine;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour
{
    public BarChange[] abilitySlots;
    public Image[] ringSlots;
    public Image armorSlot;

    private void ShowItems(Entity character)
    {
        for (byte i = 0; i < character.inventory.abilities.Length; i++)
        {
            if (character.inventory.abilities[i] != null)
            {
                abilitySlots[i].icone.sprite = character.inventory.abilities[i].stats.UI.sprite;
            }
        }
    }


    private void GetCharacter(MyEvent.OnEntityInit CharacterInstantiate)
    {
        if (CharacterInstantiate.entity.currentStats.isPlayer == true)
        {
            Entity character = CharacterInstantiate.entity;

            character.inventoryUI = this;
            ShowItems(character);
        }
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntityInit>(GetCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntityInit>(GetCharacter);
    }
}
