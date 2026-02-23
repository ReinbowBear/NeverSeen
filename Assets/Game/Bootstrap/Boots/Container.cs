using System;
using System.Collections.Generic;

public class Container
{
    public Dictionary<Type, object> SystemDatas = new();
    public Dictionary<Type, object> SceneDependencies = new();
    public Dictionary<Type, object> Services = new();

    public List<IEventListener> EventListeners = new();
    public List<IInitializable> Initializables = new();
    public List<IDisposable> Disposables = new();

    public void Add(object obj)
    {
        if (obj is ISystemData systemData) SystemDatas.Add(obj.GetType(), systemData);
        if (obj is ISceneDependency sceneDependency) SceneDependencies.Add(obj.GetType(), sceneDependency);
        if (obj is IService service) Services.Add(obj.GetType(), service);

        if (obj is IEventListener eventlistener) EventListeners.Add(eventlistener);
        if (obj is IInitializable initializable) Initializables.Add(initializable);
        if (obj is IDisposable disposable) Disposables.Add(disposable);
    }
}
