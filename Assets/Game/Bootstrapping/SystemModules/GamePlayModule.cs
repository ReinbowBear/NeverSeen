
public struct GamePlayModule : IModule
{
    public void SetTrigers(UpdatePhaseDesc phase)
    {

    }

    public void SetValidation(UpdatePhaseDesc phase)
    {

    }

    public void SetLogic(UpdatePhaseDesc phase)
    {

    }

    public void SetView(UpdatePhaseDesc phase)
    {
        phase.AddSystem<CameraDrag>();
        phase.AddSystem<CameraRotate>();
        phase.AddSystem<CameraZoom>();
    }
}
