using UnityEngine;
using UnityEngine.EventSystems;

// этот проклятый класс нужен потому что ебучий юнити не позволяет централизированно перехватывать логику инпут модуля
public class ButtonEvents : MonoBehaviour,  ISelectHandler, ISubmitHandler, IPointerEnterHandler, IPointerClickHandler,  IEventSender
{
    [Header("Внедряется кодом Init в Panel")]
    public World World;

    public void SetEventBus(World world)
    {
        World = world;
    }


    public void OnSelect(BaseEventData eventData)
    {
        //EventWorld.Invoke(gameObject, Events.UIEvents.OnNavigate);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        //EventWorld.Invoke(gameObject, Events.UIEvents.OnButtonInvoke);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //EventWorld.Invoke(gameObject, Events.UIEvents.OnNavigate);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //EventWorld.Invoke(gameObject, Events.UIEvents.OnButtonInvoke);
    }
}


//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.Events;
//using System.Collections.Generic;
//
//public class UIButtonWatcher : MonoBehaviour
//{
//    [Header("События кнопки")]
//    public UnityEvent<GameObject> OnHover;   // Наведение на кнопку
//    public UnityEvent<GameObject> OnClick;   // Клик по кнопке
//
//    private GameObject lastHovered;
//
//    void Update()
//    {
//        PointerEventData pointerData = new PointerEventData(EventSystem.current)
//        {
//            position = Input.mousePosition
//        };
//
//        // Проверяем все UI элементы под курсором
//        List<RaycastResult> results = new List<RaycastResult>();
//        EventSystem.current.RaycastAll(pointerData, results);
//
//        GameObject hoveredButton = null;
//        foreach (var result in results)
//        {
//            if (result.gameObject.GetComponent<UnityEngine.UI.Button>() != null)
//            {
//                hoveredButton = result.gameObject;
//                break;
//            }
//        }
//
//        // Наведение
//        if (hoveredButton != lastHovered)
//        {
//            lastHovered = hoveredButton;
//            if (hoveredButton != null)
//                OnHover.Invoke(hoveredButton);
//        }
//
//        // Клик
//        if (Input.GetMouseButtonDown(0) && hoveredButton != null)
//        {
//            OnClick.Invoke(hoveredButton);
//        }
//    }
//}