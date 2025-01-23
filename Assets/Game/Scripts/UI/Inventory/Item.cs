using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{    
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    [HideInInspector] public ItemSO itemSO;
    [HideInInspector] public Transform originalParent;
    private Tween tween;

    public void Init(ItemSO newItem)
    {
        itemSO = newItem;
        image.sprite = itemSO.UI.sprite;
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
        rectTransform.anchoredPosition += eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        MoveToSlot();
    }

    public void MoveToSlot()
    {
        tween = rectTransform.DOAnchorPos(originalParent.position, 0.4f).OnComplete(() => { transform.SetParent(originalParent); tween = null; });
    }


    void OnDestroy()
    {
        if (tween != null)
        {
            tween.Kill();
        }
    }
}
