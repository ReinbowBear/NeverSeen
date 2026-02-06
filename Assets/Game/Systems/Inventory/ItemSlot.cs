using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IEventSender
{
    public ItemType slotType;
    public ItemUI item;

    public EventWorld EventWorld;

    public void SetEventBus(EventWorld eventWorld)
    {
        EventWorld = eventWorld;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI newItem = eventData.pointerDrag.GetComponent<ItemUI>();

        if (slotType == ItemType.None || slotType == newItem.ItemData.ItemType)
        {
            item = newItem;
            EventWorld.Invoke(newItem.gameObject, InventoryEvents.ItemDrop);
        }
    }
}
