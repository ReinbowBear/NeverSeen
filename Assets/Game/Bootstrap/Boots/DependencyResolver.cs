using System;
using System.Reflection;

public class DependencyResolver
{
    private Container container;

    public DependencyResolver(Container systemCash)
    {
        this.container = systemCash;
    }


    public void Resolve(object instance)
    {
        var type = instance.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            var resolved = GetResolve(field.FieldType);

            if (resolved == null) continue;
            field.SetValue(instance, resolved);
        }
    }

    private object GetResolve(Type fieldType)
    {
        if (!fieldType.IsClass) return null;

        if (typeof(ISystemData).IsAssignableFrom(fieldType)) return ResolveData(fieldType);
        if (typeof(ISceneDependency).IsAssignableFrom(fieldType)) return ResolveSceneObject(fieldType);
        if (typeof(IService).IsAssignableFrom(fieldType)) return ResolveService(fieldType);

        return null;
    }


    private object ResolveData(Type type)
    {
        var cash = container.SystemDatas;
        if (cash.TryGetValue(type, out var obj)) return obj;

        obj = Activator.CreateInstance(type);
        cash[type] = obj;

        return obj;
    }

    private object ResolveSceneObject(Type type)
    {
        var cash = container.SceneDependencies;
        if (cash.TryGetValue(type, out var obj)) return obj;

        obj = UnityEngine.Object.FindFirstObjectByType(type);
        cash[type] = obj;

        return obj;
    }

    private object ResolveService(Type type)
    {
        var cash = container.Services;
        if (cash.TryGetValue(type, out var obj)) return obj;

        obj = Activator.CreateInstance(type);
        container.Add(obj);

        return obj;
    }
}
