using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class Container : IDisposable
{
    private Dictionary<Type, object> cash = new();
    private List<IDisposable> disposables = new();


    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }

    public object Resolve(Type type)
    {
        if (cash.TryGetValue(type, out var existing)) return existing;

        var instance = CreateInstance(type);
        cash[type] = instance;
        if (instance is IDisposable disposable)disposables.Add(disposable);

        return instance;
    }


    private object CreateInstance(Type type)
    {
        var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault();
        var parameters = ctor.GetParameters();
        var args = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            args[i] = Resolve(parameters[i].ParameterType);
        }

        return ctor.Invoke(args);
    }


    public void Clear()
    {
        cash.Clear();
        disposables.Clear();
    }

    public void Dispose()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }
}
