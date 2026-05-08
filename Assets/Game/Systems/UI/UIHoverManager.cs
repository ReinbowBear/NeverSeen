using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIHoverSystem : ISystem
{
    private EventSystem evtSystem;
    private PointerEventData data;
    private List<RaycastResult> results = new();

    private GameObject currentSelected;
    private GameObject last;
    private InputType lastInput;

    private MouseState mouseState;

    public UIHoverSystem(MouseState mouseState)
    {
        this.mouseState = mouseState;

        evtSystem = EventSystem.current;
        data = new PointerEventData(evtSystem);
    }


    public void Execute(World world, EntityCommands commands)
    {
        if (mouseState.IsMoved) lastInput = InputType.Mouse;
        if (world.Has<IntentNavigate>()) lastInput = InputType.Keyboard;

        if (lastInput == InputType.Mouse)
        {
            var hovered = GetPointerHover(mouseState.Position);
            if (hovered != null) currentSelected = hovered;
        }
        else
        {
            currentSelected = evtSystem.currentSelectedGameObject;
        }

        if (currentSelected == last) return;

        SendEvents(world, commands);
        Synchronize();
    }

    private GameObject GetPointerHover(Vector2 pos)
    {
        data.position = pos;

        results.Clear();
        evtSystem.RaycastAll(data, results);

        foreach (var result in results)
        {
            var selectable = result.gameObject.GetComponentInParent<UnityEngine.UI.Selectable>();
            if (selectable != null ) return selectable.gameObject;
        }

        return null;
    }


    private void SendEvents(World world, EntityCommands commands)
    {
        if (last != null)
        {
            var fromEntity = world.GetEntity(last);
            commands.AddOneFrame(fromEntity, new OnHoverExit());
        }

        if (currentSelected != null)
        {
            var toEntity = world.GetEntity(currentSelected);
            commands.AddOneFrame(toEntity, new OnHoverEnter());
        }
    }

    private void Synchronize()
    {
        if (currentSelected != evtSystem.currentSelectedGameObject)
        {
            evtSystem.SetSelectedGameObject(currentSelected);
        }

        last = currentSelected;
    }
}

public enum InputType
{
    Mouse,
    Keyboard
}
