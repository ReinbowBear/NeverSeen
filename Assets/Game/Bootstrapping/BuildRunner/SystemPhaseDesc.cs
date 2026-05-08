using System;
using System.Collections.Generic;

public class UpdatePhaseDesc // Init logic View
{
    public List<Type> SystemTypes = new();

    public void AddSystem<T>() where T : ISystem
    {
        SystemTypes.Add(typeof(T));
    }
}
