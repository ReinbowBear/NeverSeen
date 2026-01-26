using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ProxyBuilder : BaseProxy
{
    private Camera cam;
    private StateMachine stateMachine = new();

    private BuildingsBuilder buildingsBuilder = new();
    private BuilderData builderData = new();

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;

    public override void Init()
    {
        cam = Camera.main;

        pointerEventData = new PointerEventData(EventSystem.current);
        raycastResults = new();

        var mask = (LayerMask)LayerMask.GetMask("Tile");

        var view = new ViewState(builderData, mask);
        var edit = new EditState(builderData, mask);

        stateMachine.AddState(view);
        stateMachine.AddState(edit);

        //stateMachine.SetMode(); // дефолтный стейт добавить где все элементы скрыты для красивого входа? + убирать можно интерфейс так для кат сцен условно
    }

    public override void Enter()
    {
        eventWorld.AddListener(LeftClick, GamePlayInputEvents.LeftClick);
        eventWorld.AddListener(RightClick, GamePlayInputEvents.RightClick);
    }

    public override void Exit()
    {
        eventWorld.RemoveListener(LeftClick, GamePlayInputEvents.LeftClick);
        eventWorld.RemoveListener(RightClick, GamePlayInputEvents.RightClick);
    }


    public void SetMode()
    {

    }


    public void LeftClick()
    {
        if (IsPointerOverUI()) return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        //if (Physics.Raycast(ray, out var hit, 30, layerMask, QueryTriggerInteraction.Ignore))
        //{
        //    //Select(hit.collider.gameObject);
        //}
    }


    private void RightClick() => StartCoroutine(TryRightClick());
    private IEnumerator TryRightClick()
    {
        Vector3 mousePos = UnityEngine.Input.mousePosition;

        //while (input.GamePlay.MouseRight.IsPressed()) yield return null;
        yield return null;

        if ((mousePos - UnityEngine.Input.mousePosition).magnitude <= 100f) // срабатывает если разница не более number float
        {
            //CurrentState.RightClick();
        }
    }


    private bool IsPointerOverUI()
    {
        pointerEventData.position = Mouse.current.position.ReadValue();

        raycastResults.Clear();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
    }
}

public interface IViewMode
{
    void LeftClick(RaycastHit hit);
    void RightClick();
}
