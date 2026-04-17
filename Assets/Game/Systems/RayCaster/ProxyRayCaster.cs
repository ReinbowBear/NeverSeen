using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ProxyRayCaster : MonoBehaviour, IEventListener
{
    private Camera cam;

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults = new();

    public LayerMask RayLayer;
    public float RayDistance = 30;

    private World world;

    void Awake()
    {
        cam = Camera.main;
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    public void SetEvents(World world)
    {
        //eventWorld.AddListener(Select, Events.GamePlayInput.LeftClick);
        //eventWorld.AddListener(Deselect, Events.GamePlayInput.RightClick);
    }


    private void Select() // нам нужен механизм передачи фиксированных аргументов?
    {
        if (IsPointerOverUI()) return;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, RayDistance, RayLayer, QueryTriggerInteraction.Ignore))
        {
            var obj = hit.transform.gameObject;
            //eventWorld.Invoke(obj, Events.ObjectEvents.Select);
        }
    }

    private void Deselect()
    {
        if (IsPointerOverUI()) return;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, RayDistance, RayLayer, QueryTriggerInteraction.Ignore))
        {
            //eventWorld.Invoke(Events.ObjectEvents.Deselect);
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
