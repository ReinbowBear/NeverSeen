using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemType slotType;
    private ItemUI item;

    public ItemUI GetItem()
    {
        if (item == null)
        {
            item = GetComponentInChildren<ItemUI>();
        }
        return item;
    }


    private void SwapItems(ItemUI newItem)
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
        ItemUI newItem = eventData.pointerDrag.GetComponent<ItemUI>();

        if (slotType == ItemType.None || slotType.ToString() == newItem.itemSO.GetType().Name)
        {
            SwapItems(newItem);
        }
    }
}
