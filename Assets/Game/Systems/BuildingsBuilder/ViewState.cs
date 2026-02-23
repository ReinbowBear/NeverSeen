
public class ViewState : IViewMode, IState
{
    public EventWorld EventWorld;
    public BuilderData BuilderData;

    public ViewState(EventWorld eventWorld, BuilderData builderData)
    {
        EventWorld = eventWorld;
        BuilderData = builderData;
    }

    public void Enter()
    {
        EventWorld.Invoke(BuilderEvents.ViewModeEnter);
    }

    public void Exit()
    {
        RightClick();
    }


    public void LeftClick(Tile tile)
    {
        var obj = tile.TileData.IsTaken;
        if (obj == null) return;

        BuilderData.CurrentBuilding = obj;
        EventWorld.Invoke(obj, BuilderEvents.Select);
    }

    public void RightClick()
    {
        var obj = BuilderData.CurrentBuilding;
        if (obj == null) return;
        
        obj = null;
        EventWorld.Invoke(BuilderEvents.Deselect);
    }
}
