using UnityEngine;
using UnityEngine.EventSystems;

// этот проклятый класс нужен потому что ебучий юнити не позволяет централизированно перехватывать логику инпут модуля
public class ButtonEvents : MonoBehaviour,  ISelectHandler, ISubmitHandler, IPointerEnterHandler, IPointerClickHandler,  IEventSender
{
    [Header("Внедряется кодом Init в Panel")]
    public EventWorld EventWorld { get; set; }

    public void SetEventBus(EventWorld eventWorld)
    {
        this.EventWorld = eventWorld;
    }


    public void OnSelect(BaseEventData eventData)
    {
        EventWorld.Invoke(gameObject, Events.UIEvents.OnNavigate);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        EventWorld.Invoke(gameObject, Events.UIEvents.OnButtonInvoke);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        EventWorld.Invoke(gameObject, Events.UIEvents.OnNavigate);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventWorld.Invoke(gameObject, Events.UIEvents.OnButtonInvoke);
    }
}
