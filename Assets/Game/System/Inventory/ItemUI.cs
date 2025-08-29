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

    [HideInInspector] public ItemData item;
    [HideInInspector] public Transform originalParent;
    private Vector3 originalpos;
    private Tween tween;

    public void Init(ItemData newItem)
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
        Vector3 targetPosition = UnityEngine.Input.mousePosition;
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
