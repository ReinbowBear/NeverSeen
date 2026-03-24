using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class SystemActivator
{
    private SystemDescriptor[] cachedTypes;


    public void CacheTypes()
    {
        cachedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsClass && !type.IsAbstract)
        .Where(type => type.GetCustomAttribute<GameSystemAttribute>() != null)
        .Select(type => new SystemDescriptor(type, type.GetCustomAttribute<GameSystemAttribute>()))
        .ToArray();
    }


    public void SortTypes()
    {
        var systems = cachedTypes.OrderBy(x => x.Attribute.SystemGroup).ToList();
        var typeMap = systems.ToDictionary(x => x.GetType());

        var graph = systems.ToDictionary(x => x, desk => new List<SystemDescriptor>());
        var indegree = systems.ToDictionary(x => x, desk => 0);

        foreach (var system in systems)
        {
            var type = system.GetType();

            foreach (var attr in type.GetCustomAttributes<UpdateBeforeAttribute>())
            {
                if (!typeMap.TryGetValue(attr.TargetSystem, out var target)) continue;

                graph[system].Add(target);
                indegree[target]++;
            }

            foreach (var attr in type.GetCustomAttributes<UpdateAfterAttribute>())
            {
                if (!typeMap.TryGetValue(attr.TargetSystem, out var target)) continue;

                graph[target].Add(system);
                indegree[system]++;
            }
        }

        var queue = new Queue<SystemDescriptor>(indegree.Where(x => x.Value == 0).Select(x => x.Key));
        var result = new List<SystemDescriptor>();

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node);

            foreach (var dep in graph[node])
            {
                indegree[dep]--;

                if (indegree[dep] == 0)
                {
                    queue.Enqueue(dep);
                }
            }
        }

        if (result.Count != systems.Count) throw new Exception("System update order contains cycle");
        cachedTypes = result.ToArray();
    }


    public IEnumerable<object> GetSystems(UpdateState state)
    {
        foreach (var descriptor in cachedTypes)
        {
            var systemUpdateOn = descriptor.Attribute.UpdateState;
            if (systemUpdateOn == state || systemUpdateOn == UpdateState.Global)
            {
                yield return descriptor.GetInstance();
            }
        }
    }
}


public class SystemDescriptor
{
    public Type SystemType;
    public GameSystemAttribute Attribute;
    public object Instance;

    public SystemDescriptor(Type systemType, GameSystemAttribute attribute)
    {
        SystemType = systemType;
        Attribute = attribute;
    }

    
    public object GetInstance()
    {
        if(Instance != null) return Instance;

        Instance= Activator.CreateInstance(SystemType);
        return Instance;
    }
}
