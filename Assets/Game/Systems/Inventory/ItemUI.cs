using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler,  IEventSender
{    
    public Image image;
    public ItemData ItemData;

    public World World;

    public void Init(ItemData newItem)
    {
        ItemData = newItem;
        image.sprite = ItemData.Sprite;
    }

    public void SetEventBus(World world)
    {
        World = world;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //EventWorld.Invoke(gameObject, InventoryEvents.ItemBeginDrag);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //EventWorld.Invoke(gameObject, InventoryEvents.ItemDrag);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //EventWorld.Invoke(gameObject, InventoryEvents.ItemEndDrag);
    }
}
