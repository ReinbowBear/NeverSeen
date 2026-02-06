using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ProxyRayCaster : BaseProxy
{
    private Camera cam;

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults = new();

    public LayerMask RayLayer;
    public float RayDistance = 30;

    public override void Init()
    {
        cam = Camera.main;
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    public override void Enter() // благодаря события можно установить список на что скрипт реагирует и что после этого запускает в случаи рейкаста
    {
        eventWorld.AddListener(Select, Events.GamePlayInput.LeftClick);
        eventWorld.AddListener(Deselect, Events.GamePlayInput.RightClick);
    }

    public override void Exit()
    {
        eventWorld.RemoveListener(Select, Events.GamePlayInput.LeftClick);
        eventWorld.RemoveListener(Deselect, Events.GamePlayInput.RightClick);
    }


    private void Select() // нам нужен механизм передачи фиксированных аргументов?
    {
        if (IsPointerOverUI()) return;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, RayDistance, RayLayer, QueryTriggerInteraction.Ignore))
        {
            var obj = hit.transform.gameObject;
            eventWorld.Invoke(obj, Events.RayCaster.Select);
        }
    }

    private void Deselect()
    {
        if (IsPointerOverUI()) return;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, RayDistance, RayLayer, QueryTriggerInteraction.Ignore))
        {
            eventWorld.Invoke(Events.RayCaster.Deselect);
        }
    }


    private bool IsPointerOverUI()
    {
        raycastResults.Clear();

        pointerEventData.position = Mouse.current.position.ReadValue();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
    }
}
