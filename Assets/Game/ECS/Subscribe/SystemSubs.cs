using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemSubs
{
    public List<ISubscribe> Subscribes = new();

    public CommandSubscribe AddWithCommands(Action<EntityCommands> callback)
    {
        var sub = new CommandSubscribe(callback);
        Subscribes.Add(sub);
        return sub;
    }

    public CommandSubscribe<T1> AddWithCommands<T1>(Action<EntityCommands, T1> callback)
    {
        var sub = new CommandSubscribe<T1>(callback);
        Subscribes.Add(sub);
        return sub;
    }

    public CommandSubscribe<T1, T2> AddWithCommands<T1, T2>(Action<EntityCommands, T1, T2> callback)
    {
        var sub = new CommandSubscribe<T1, T2>(callback);
        Subscribes.Add(sub);
        return sub;
    }


    public Subscribe AddListener(Action callback)
    {
        var sub = new Subscribe(callback);
        Subscribes.Add(sub);
        return sub;
    }

    public Subscribe<T1> AddListener<T1>(Action<T1> callback)
    {
        var sub = new Subscribe<T1>(callback);
        Subscribes.Add(sub);
        return sub;
    }

    public Subscribe<T1, T2> AddListener<T1, T2>(Action<T1, T2> callback)
    {
        var sub = new Subscribe<T1, T2>(callback);
        Subscribes.Add(sub);
        return sub;
    }
}


public struct DataRequire
{
    private World world;
    private GameObject obj;
    private Entity entity;

    public DataRequire(World world)
    {
        this.world = world;
        obj = new();
        entity = world.CreateEntity(obj);
    }


    public void AddToWorld<T>() where T : new()
    {
        if (world.HasEntityWith(typeof(T))) return;
        world.AddComponent(entity, new T());
    }
}

//foreach (var desc in activeSystems)
//{
//    if (desc.System is not IDataRequire require) return;
//    var dataSetter = new DataRequire(world);
//    require.SetData(dataSetter);
//}

public interface IDataRequire
{
    void SetData(DataRequire dataRequire);
}
