using UnityEngine;

public class ProxyInventory : MonoBehaviour, IEventListener
{
    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener<ItemUI>(this, InsertOnSlot, InventoryEvents.ItemDrop);

        eventWorld.AddListener<CanvasGroup>(this, OnBeginDrag, InventoryEvents.ItemBeginDrag);
        eventWorld.AddListener<RectTransform>(this, OnDrag, InventoryEvents.ItemDrag);
        eventWorld.AddListener<CanvasGroup>(this, OnEndDrag, InventoryEvents.ItemEndDrag);
    }


    private void InsertOnSlot(ItemUI newItem) // надо проверить обновляем ли мы значение старого слота что предмета там УЖЕ НЕТ
    {
        // свапнуть предметы если слот заполнен и это возможно else вернуть предмет
        // просто вставить предмет если слот пустой и это возможно
    }

    // item UI
    public void OnBeginDrag(CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = false; // отключаем захват рейкастов, предмет ловит рейкасты на себя, не пропуская проверки на OnDrop
        
        //originalParent = transform.parent; // нужно помнить начальный слот айтема что бы в случаи чего вернуть его туда
        canvasGroup.transform.SetParent(transform.root);
    }

    public void OnDrag(RectTransform rectTransform)
    {
        Vector3 targetPosition = UnityEngine.Input.mousePosition;
        rectTransform.position = targetPosition;
    }

    public void OnEndDrag(CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = true;
        //canvasGroup.rectTransform.DOAnchorPos(originalpos, 0.4f).OnComplete(() => { canvasGroup.transform.SetParent(originalParent); });;
    }
}

public enum InventoryEvents
{
    ItemDrop,

    ItemBeginDrag,
    ItemDrag,
    ItemEndDrag
}
