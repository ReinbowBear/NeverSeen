using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;


    public void OnPointerEnter(PointerEventData eventData = null)
    {
        OnEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData = null)
    {
        OnExit?.Invoke();
    }
}
