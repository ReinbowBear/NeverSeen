using System;
using System.Collections.Generic;
using System.Reflection;

public class Container : IDisposable
{
    public Dictionary<Type, object> SceneDependencies = new();
    public Dictionary<Type, object> SystemDatas = new();
    public Dictionary<Type, object> Services = new();

    public List<IDisposable> Disposables = new();


    public void Register(object obj)
    {
        var type = obj.GetType();
        
        if (type.IsDefined(typeof(SceneDependencyAttribute), false)) SceneDependencies.Add(type, obj);
        if (type.IsDefined(typeof(SystemDataAttribute), false)) SystemDatas.Add(type, obj);
        if (type.IsDefined(typeof(ServiceAttribute), false)) Services.Add(type, obj);

        if (obj is IDisposable disposable) Disposables.Add(disposable);
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

        if (fieldType.IsDefined(typeof(SystemDataAttribute), false)) return ResolveData(fieldType);
        if (fieldType.IsDefined(typeof(SceneDependencyAttribute), false)) return ResolveSceneObject(fieldType);
        if (fieldType.IsDefined(typeof(ServiceAttribute), false)) return ResolveService(fieldType);

        return null;
    }


    private object ResolveData(Type type)
    {
        if (SystemDatas.TryGetValue(type, out var obj)) return obj;

        obj = Activator.CreateInstance(type);
        SystemDatas[type] = obj;

        return obj;
    }

    private object ResolveSceneObject(Type type)
    {
        if (SceneDependencies.TryGetValue(type, out var obj)) return obj;

        obj = UnityEngine.Object.FindFirstObjectByType(type);
        SceneDependencies[type] = obj;

        return obj;
    }

    private object ResolveService(Type type)
    {
        if (Services.TryGetValue(type, out var obj)) return obj;

        obj = Activator.CreateInstance(type);
        if (obj is IDisposable disposable) Disposables.Add(disposable);

        return obj;
    }


    public void Clear()
    {
        SceneDependencies.Clear();
        SystemDatas.Clear();
        Services.Clear();

        Disposables.Clear();
    }

    public void Dispose()
    {
        foreach (var disposable in Disposables)
        {
            disposable.Dispose();
        }
    }
}
