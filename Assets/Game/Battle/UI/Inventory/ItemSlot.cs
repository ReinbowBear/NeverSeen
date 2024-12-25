using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemContainer.ItemType slotType;
    private Item item;

    public Item GetItem()
    {
        if (item == null)
        {
            item = GetComponentInChildren<Item>();
        }
        return item;
    }


    private void SwapItems(Item newItem)
    {
        if (GetItem() != null)
        {
            item.originalParent = newItem.originalParent;
            item.MoveToSlot();
        }

        newItem.originalParent = transform; //дальше Item передвинет его собсвтенный MoveToSlot()
    }


    public void OnDrop(PointerEventData eventData)
    {
        Item newItem = eventData.pointerDrag.GetComponent<Item>();

        if (slotType == ItemContainer.ItemType.none || slotType == newItem.container.itemType)
        {
            SwapItems(newItem);
        }
    }
}
