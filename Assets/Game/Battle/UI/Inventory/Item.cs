using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{    
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemName;
    [Space]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    [HideInInspector] public ItemContainer container;
    [HideInInspector] public Transform originalParent;
    private Tween tween;

    public void Init(Container newItem)
    {
        container = newItem as ItemContainer;
        
        image.sprite = container.UI.sprite;
        itemName.text = container.UI.itemName;
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
        tween = rectTransform.DOAnchorPos(originalParent.position, 0.2f).OnComplete(() => { transform.SetParent(originalParent); tween = null; });
    }


    void OnDestroy()
    {
        if (tween != null)
        {
            tween.Kill();
        }
    }
}