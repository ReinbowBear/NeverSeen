using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IDropHandler
{    
    public Item item => GetComponentInChildren<Item>();
    [SerializeField] private ItemSO.ItemType myItemType;

    public void AddItem(Item newItem)
    {
        if (newItem.itemSO.itemType == myItemType)
        {
            newItem.originalParent = transform;
        }
    }

    private void SwapItems(Item newItem)
    {
        if (newItem.itemSO.itemType == myItemType)
        {
            item.transform.SetParent(newItem.originalParent, false);
            newItem.originalParent = transform;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (item == null)
        {
            AddItem(eventData.pointerDrag.GetComponent<Item>());
        }
        else
        {
            SwapItems(eventData.pointerDrag.GetComponent<Item>());
        }
    }
}
