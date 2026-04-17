using System;
using System.Collections.Generic;

public class SystemPhase // Init logic View
{
    public List<Type> SystemTypes = new();
    public List<SystemSubs> SystemSubs = new();


    public void AddSystem<T>() where T : ISystem
    {
        SystemTypes.Add(typeof(T));
    }
}
