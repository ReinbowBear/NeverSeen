
public class MenuSystems : ISystemGroupDesc
{
    public void SetInit(SystemPhase phase)
    {

    }

    public void SetLogic(SystemPhase phase)
    {
        phase.AddSystem<PanelControl>();
    }

    public void SetView(SystemPhase phase)
    {
        phase.AddSystem<PanelNavigate>();
    }
}
