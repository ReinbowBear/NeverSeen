using System;
using System.Collections.Generic;

public sealed class SystemRunner
{
    private readonly List<ISystem> systems = new();

    private World world;
    private FilterRegistry filterRegistry;

    public SystemRunner(World world, FilterRegistry filterRegistry)
    {
        this.world = world;
        this.filterRegistry = filterRegistry;
    }


    public void AddSystem(ISystem newSystem)
    {
        systems.Add(newSystem);
    }
    
    public void RemoveSystem(ISystem oldSystem)
    {   
        systems.Remove(oldSystem);
    }


    public void Update()
    {
        foreach (var system in systems)
        {
            var type = system.GetType();
            var filter = filterRegistry.GetFilter(type);

            if (filter.Entities.Length == 0) continue;
            system.Update(world, filter);
        }
    }
}

public class SystemContext : IEquatable<SystemContext>
{
    public ISystem System;
    public Filter Filter;

    public SystemContext(ISystem system, Filter filter)
    {
        System = system;
        Filter = filter;
    }

    public bool Equals(SystemContext other)
    {
        if (other is null) return false;
        return ReferenceEquals(this, other);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as SystemContext);
    }

    public override int GetHashCode() // сравнение идёт по переменной System
    {
        return System.GetHashCode();
    }
}
