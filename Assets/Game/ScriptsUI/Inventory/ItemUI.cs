using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{    
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    [HideInInspector] public Item item;
    [HideInInspector] public Transform originalParent;
    private Vector3 originalpos;
    private Tween tween;

    public void Init(Item newItem)
    {
        item = newItem;
        image.sprite = item.UI.sprite;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (tween != null)
        {
            tween.Kill();
        }
        canvasGroup.blocksRaycasts = false; //отключаем захват рейкастов, предмет ловит рейкасты на себя, не пропуская проверки на OnDrop
        
        originalParent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 targetPosition = Input.mousePosition;
        rectTransform.position = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(originalParent); // во время движения объект уже прикреплён к своему слоту, от чего другие слоты по иерархии объектов в сцене, отображаются выше него!
        MoveToSlot();
    }

    public void MoveToSlot()
    {
        tween = rectTransform.DOAnchorPos(originalpos, 0.4f).OnComplete(() => { tween = null; });
    }


    void OnDestroy()
    {
        if (tween != null)
        {
            tween.Kill();
        }
    }
}


//public class ItemFactory
//{
//    public static async Task<ItemUI> GetItem(Item itemData)
//    {
//        var handle = Addressables.LoadAssetAsync<GameObject>("ItemUI");
//        await handle.Task;
//
//        var release = handle.Result.AddComponent<ReleaseOnDestroy>();
//        release.handle = handle;
//
//        ItemUI newItem = handle.Result.GetComponent<ItemUI>();
//        newItem.Init(itemData);
//        
//        return newItem;
//    }
//}