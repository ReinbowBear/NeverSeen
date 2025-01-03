using UnityEngine;

public class EquipmentControl : MonoBehaviour
{
    [SerializeField] private Entity character;

    public void ApplyEquipment(MyEvent.OnEntryBattle _)
    {
        //for (byte i = 0; i < character.inventory.rings.Length; i++)
        //{
        //    if (character.inventory.rings[i].GetItem() != null)
        //    {
        //        //character.inventory.rings[i].GetItem().
        //    }
        //}
    }

    public void FalseEquipment()
    {
        //for (byte i = 0; i < character.inventory.rings.Length; i++)
        //{
        //    if (character.inventory.rings[i].GetItem() != null)
        //    {
        //        //character.inventory.rings[i].GetItem().
        //    }
        //}
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryBattle>(ApplyEquipment);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryBattle>(ApplyEquipment);
    }
}
