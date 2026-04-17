
public class CoreSystems : ISystemGroupDesc
{
    public void SetInit(SystemPhase phase)
    {
        phase.AddSystem<Input>();
    }

    public void SetLogic(SystemPhase phase)
    {

    }

    public void SetView(SystemPhase phase)
    {
        phase.AddSystem<Tween>();
        phase.AddSystem<Audio>();
        phase.AddSystem<SceneLoader>();
    }
}
