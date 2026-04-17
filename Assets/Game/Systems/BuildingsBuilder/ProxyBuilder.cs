using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProxyBuilder : MonoBehaviour, IEventListener
{
    private StateMachine<Type, IState> stateMachine = new();
    private BuilderData builderData = new();

    private Vector2 mousePos;
    public float MouseMagnitude = 100f;

    private World world;


    public ProxyBuilder()
    {
        var view = new ViewState(world, builderData);
        var edit = new EditState(world, builderData);

        stateMachine.AddState(view.GetType(), view);
        stateMachine.AddState(edit.GetType(), edit);

        //stateMachine.SetMode(); // дефолтный стейт добавить где все элементы скрыты для красивого входа? + убирать можно интерфейс так для кат сцен условно
    }

    public void SetEvents(World world)
    {
        //eventWorld.AddListener<Tile>(LeftClick, Events.ObjectEvents.Select);
        //eventWorld.AddListener(SaveMousePos, Events.ObjectEvents.Deselect);

        //eventWorld.AddListener(this, AfterRightClick, Events.GamePlayInput.RightClickCancel);
    }


    public void SetMode(IState state)
    {
        stateMachine.SetState(state.GetType());
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
