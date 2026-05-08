
public struct CoreModule : IModule
{
    public void SetTrigers(UpdatePhaseDesc phase)
    {
        phase.AddSystem<Input>();
        phase.AddSystem<MouseInput>();
    }
    
    public void SetValidation(UpdatePhaseDesc phase)
    {

    }

    public void SetLogic(UpdatePhaseDesc phase)
    {

    }

    public void SetView(UpdatePhaseDesc phase)
    {
        phase.AddSystem<Tween>();
        phase.AddSystem<FadeSystem>();
    }
}
