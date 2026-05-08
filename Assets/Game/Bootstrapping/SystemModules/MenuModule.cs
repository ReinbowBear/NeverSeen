
public struct MenuModule : IModule
{
    public void SetTrigers(UpdatePhaseDesc phase)
    {

    }
    
    public void SetValidation(UpdatePhaseDesc phase)
    {
        phase.AddSystem<UIHoverSystem>();
    }

    public void SetLogic(UpdatePhaseDesc phase)
    {
        phase.AddSystem<PanelControl>();
    }

    public void SetView(UpdatePhaseDesc phase)
    {
        phase.AddSystem<PanelNavigator>();
        phase.AddSystem<MenuSounds>();
    }
}
