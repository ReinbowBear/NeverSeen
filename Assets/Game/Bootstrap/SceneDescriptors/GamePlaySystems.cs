
public class GamePlaySystems : ISystemGroupDesc
{
    public void SetInit(SystemPhase phase)
    {

    }

    public void SetLogic(SystemPhase phase)
    {

    }

    public void SetView(SystemPhase phase)
    {
        phase.AddSystem<CameraDrag>();
        phase.AddSystem<CameraRotate>();
        phase.AddSystem<CameraZoom>();
    }
}
