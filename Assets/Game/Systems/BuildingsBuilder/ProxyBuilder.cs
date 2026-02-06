using UnityEngine;
using UnityEngine.InputSystem;

public class ProxyBuilder : BaseProxy
{
    private StateMachine stateMachine = new();
    private BuilderData builderData = new();

    private Vector2 mousePos;
    public float MouseMagnitude = 100f;


    public override void Init()
    {
        var view = new ViewState(eventWorld, builderData);
        var edit = new EditState(eventWorld, builderData);

        stateMachine.AddState(view);
        stateMachine.AddState(edit);

        //stateMachine.SetMode(); // дефолтный стейт добавить где все элементы скрыты для красивого входа? + убирать можно интерфейс так для кат сцен условно
    }

    public override void Enter()
    {
        eventWorld.AddListener<Tile>(LeftClick, Events.RayCaster.Select);
        eventWorld.AddListener(SaveMousePos, Events.RayCaster.Deselect);

        eventWorld.AddListener(AfterRightClick, Events.GamePlayInput.RightClickCancel);
    }

    public override void Exit()
    {
        eventWorld.RemoveListener<Tile>(LeftClick, Events.RayCaster.Select);
        eventWorld.RemoveListener(SaveMousePos, Events.RayCaster.Deselect);

        eventWorld.RemoveListener(AfterRightClick, Events.GamePlayInput.RightClickCancel);
    }


    public void SetMode(IState state)
    {
        stateMachine.SetMode(state);
    }


    private void LeftClick(Tile tile)
    {
        var mode = stateMachine.CurrentState as IViewMode;
        mode.LeftClick(tile);
    }

    private void SaveMousePos()
    {
        if (builderData.CurrentBuilding == null) return;
        mousePos = Mouse.current.position.ReadValue();
    }

    private void AfterRightClick()
    {
        float distance = Vector2.Distance(mousePos, mousePos);

        if (distance <= MouseMagnitude)
        {
            var viewMode = stateMachine.CurrentState as IViewMode;
            viewMode.RightClick();
        }
    }
}

public interface IViewMode
{
    void LeftClick(Tile tile);
    void RightClick();
}

public enum BuilderEvents
{
    Select,
    Deselect,

    Spawn,
    Destroy,

    NoneModeEnter,
    ViewModeEnter,
    EditModeEnter,
}
