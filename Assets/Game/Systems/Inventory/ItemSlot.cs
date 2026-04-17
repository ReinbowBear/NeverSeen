using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IEventSender
{
    public ItemType slotType;
    public ItemUI item;

    public World World;

    public void SetEventBus(World world)
    {
        World = world;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI newItem = eventData.pointerDrag.GetComponent<ItemUI>();

        if (slotType == ItemType.None || slotType == newItem.ItemData.ItemType)
        {
            item = newItem;
            //EventWorld.Invoke(newItem.gameObject, InventoryEvents.ItemDrop);
        }
    }
}
