using System.Collections.Generic;

public class SystemGroup
{
    public List<ISystem>[] Phases;

    public SystemGroup()
    {
        Phases = new List<ISystem>[4]
        {
            new(),
            new(),
            new(),
            new()
        };
    }


    public List<ISystem> GetPhase(Phase phase)
    {
        int index = (int)phase;
        return Phases[index];
    }

    public void Update(Phase phase, World world, EntityCommands commands)
    {
        foreach (var system in GetPhase(phase))
        {
            system.Execute(world, commands);
        }
    }



    public IEnumerator<ISystem> GetEnumerator()
    {
        foreach (var phase in Phases)
        {
            foreach (var system in phase)
            {
                yield return system;
            }
        }
    }
}

public enum Phase
{
    Triger,
    Valid,
    Logic,
    View
}
