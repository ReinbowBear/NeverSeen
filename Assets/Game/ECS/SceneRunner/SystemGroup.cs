using System.Collections.Generic;

public class SystemGroup
{
    private Dictionary<UpdatePhase, SystemPhase> phases = new();
    public Dictionary<UpdatePhase, SystemPhase>.ValueCollection Phases => phases.Values;

    public SystemGroup(ISystemGroupDesc desc)
    {
        desc.SetInit(GetPhase(UpdatePhase.Init));        
        desc.SetLogic(GetPhase(UpdatePhase.Logic));
        desc.SetView(GetPhase(UpdatePhase.View));
    }


    public SystemPhase GetPhase(UpdatePhase updatePhase)
    {
        if (phases.TryGetValue(updatePhase, out var phase)) return phase;

        phase = new();
        phases[updatePhase] = phase;
        return phase;
    }


    public void Update(World world, UpdatePhase updatePhase)
    {
        foreach (var Subs in phases[updatePhase].SystemSubs)
        {
            foreach (var sub in Subs.Subscribes)
            {
                sub.Execute(world);
            }
        }
    }


    public void Clear()
    {
        foreach (var phase in phases.Values)
        {
            phase.SystemSubs.Clear();
        }
    }
}

public enum UpdatePhase
{
    Init,
    Logic,
    View
}
