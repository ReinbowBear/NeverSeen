
public class ViewState : IViewMode, IState
{
    public World World;
    public BuilderData BuilderData;

    public ViewState(World world, BuilderData builderData)
    {
        World = world;
        BuilderData = builderData;
    }

    public void Enter()
    {
        //EventWorld.Invoke(BuilderEvents.ViewModeEnter);
    }

    public void Exit()
    {
        RightClick();
    }


    public void LeftClick(Tile tile)
    {
        var obj = tile.IsTaken;
        if (obj == null) return;

        BuilderData.CurrentBuilding = obj;
        //EventWorld.Invoke(obj, BuilderEvents.Select);
    }

    public void RightClick()
    {
        var obj = BuilderData.CurrentBuilding;
        if (obj == null) return;
        
        obj = null;
        //EventWorld.Invoke(BuilderEvents.Deselect);
    }
}
