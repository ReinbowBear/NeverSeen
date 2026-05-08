
public class SystemGroupDesc
{
    public UpdatePhaseDesc[] Phases = new UpdatePhaseDesc[4];

    public SystemGroupDesc(IModule desc)
    {
        desc.SetTrigers(GetPhase(Phase.Triger));        
        desc.SetValidation(GetPhase(Phase.Valid));        
        desc.SetLogic(GetPhase(Phase.Logic));
        desc.SetView(GetPhase(Phase.View));
    }


    public UpdatePhaseDesc GetPhase(Phase phase)
    {
        int index = (int)phase;
        return Phases[index] ??= new UpdatePhaseDesc();
    }
}
