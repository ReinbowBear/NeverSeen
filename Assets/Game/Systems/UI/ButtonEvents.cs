using UnityEngine;
using UnityEngine.EventSystems;

// этот проклятый класс нужен потому что ебучий юнити не позволяет централизированно перехватывать логику инпут модуля
public class ButtonEvents : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Внедряется кодом Init в Panel")]
    public EventWorld eventWorld;

    public void SetEventBus(EventWorld eventWorld)
    {
        this.eventWorld = eventWorld;
    }


    public void OnSelect(BaseEventData eventData)
    {
        eventWorld.Invoke(gameObject, Events.UIEvents.OnNavigate);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        eventWorld.Invoke(gameObject, Events.UIEvents.OnButtonInvoke);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        eventWorld.Invoke(gameObject, Events.UIEvents.OnNavigate);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventWorld.Invoke(gameObject, Events.UIEvents.OnButtonInvoke);
    }
}
