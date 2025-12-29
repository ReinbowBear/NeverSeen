using System.Collections.Generic;

public sealed class SystemRunner
{
    private readonly List<SystemDescription> systems = new();


    public void AddSystem(ISystem system, float updateInterval = 0f)
    {
        var handle = new SystemDescription(system, updateInterval);
        systems.Add(handle);
    }

    public void RemoveSystem(ISystem system)
    {
        systems.RemoveAll(s => s.System == system);
    }


    public void Update(World world, float deltaTime)
    {
        foreach (var systemDesc in systems)
        {
            systemDesc.TimeAccumulator += deltaTime;

            if (systemDesc.TimeAccumulator < systemDesc.UpdateInterval) continue;

            systemDesc.TimeAccumulator = 0f;
            systemDesc.System.Update(world);
        }
    }
}


internal sealed class SystemDescription
{
    public readonly ISystem System;

    public float UpdateInterval; // 0 = каждый кадр
    public float TimeAccumulator;

    public SystemDescription(ISystem system, float updateInterval)
    {
        System = system;
        UpdateInterval = updateInterval;
    }
}
