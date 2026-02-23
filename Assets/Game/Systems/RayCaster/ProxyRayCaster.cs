using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ProxyRayCaster : MonoBehaviour, IInitializable, IEventListener
{
    private Camera cam;

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults = new();

    public LayerMask RayLayer;
    public float RayDistance = 30;

    private EventWorld eventWorld;

    public void Init()
    {
        cam = Camera.main;
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener(this, Select, Events.GamePlayInput.LeftClick);
        eventWorld.AddListener(this, Deselect, Events.GamePlayInput.RightClick);
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
