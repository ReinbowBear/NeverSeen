using UnityEngine;

public class EditState : IViewMode, IState
{
    public EventWorld EventWorld;
    public BuilderData BuilderData;

    public EditState(EventWorld eventWorld, BuilderData builderData)
    {
        EventWorld = eventWorld;
        BuilderData = builderData;
    }

    public void Enter()
    {
        EventWorld.Invoke(BuilderEvents.EditModeEnter);
    }

    public void Exit()
    {
        RightClick();
    }


    public void LeftClick(Tile tile)
    {
        var obj = BuilderData.CurrentBuilding;
        if (obj == null) return;

        EventWorld.Invoke(obj, BuilderEvents.Spawn);
    }

    public void RightClick()
    {
        var obj = BuilderData.CurrentBuilding;
        if (obj == null) return;

        BuilderData.CurrentBuilding = null;
        EventWorld.Invoke(obj, BuilderEvents.Destroy);
    }
}
