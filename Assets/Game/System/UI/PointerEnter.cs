using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private bool isCursorInside;

    public void OnPointerEnter(PointerEventData eventData = null)
    {
        if (isCursorInside) return;

        isCursorInside = true;
        OnEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData = null)
    {
        if (!isCursorInside) return;

        isCursorInside = false;
        OnExit?.Invoke();
    }
}
