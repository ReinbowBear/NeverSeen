using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class DependencyResolver
{
    const string injectMethodName = "Inject";
    private Dictionary<Type, object> cache = new();

    public void Register(object instance)
    {
        cache[instance.GetType()] = instance;
    }


    public void Resolve(object instance)
    {
        var type = instance.GetType();
        var method = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(method => method.Name == injectMethodName);

        if (method == null) return;

        var args = method.GetParameters().Select(param => GetDependency(param.ParameterType)).ToArray();

        method.Invoke(instance, args);
    }

    private object GetDependency(Type type)
    {
        if (cache.TryGetValue(type, out var obj)) return obj;

        obj = Activator.CreateInstance(type);
        cache[type] = obj;

        Resolve(obj);
        return obj;
    }
}
