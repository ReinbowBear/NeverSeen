using System;
using System.Collections.Generic;
using UnityEngine;

#region T0
public class Subscribe : ISubscribe
{
    private Action Callback;
    private List<Type> OnEvents = new();

    public Subscribe(Action callback)
    {
        Callback = callback;
    }

    public void BuildQuery(World world)
    {

    }


    public void OnEvent<T1>()
    {
        OnEvents.Add(typeof(T1));
    }

    public void OnEvent<T1, T2>()
    {
        OnEvents.Add(typeof(T1));
        OnEvents.Add(typeof(T2));
    }

    public void OnEvent<T1, T2, T3>()
    {
        OnEvents.Add(typeof(T1));
        OnEvents.Add(typeof(T2));
        OnEvents.Add(typeof(T3));
    }


    public void Execute(World world)
    {
        foreach (var type in OnEvents)
        {
            if (!world.HasEntityWith(type)) return;
        }

        Callback.Invoke();
    }
}
#endregion

#region T1
public class Subscribe<T1> : ISubscribe
{
    private Action<T1> Callback;
    private Query<T1> query;
    private List<Type> OnEvents = new();

    public Subscribe(Action<T1> callback)
    {
        Callback = callback;
    }

    public void BuildQuery(World world)
    {
        query = world.Query<T1>();
        query.GetEnumerator();
    }

    public Query<T1> Require<T>()
    {
        query.Require<T>();
        return query;
    }

    public Query<T1> Exclude<T>()
    {
        query.Exclude<T>();
        return query;
    }


    public void OnEvent<TA>()
    {
        OnEvents.Add(typeof(TA));
    }

    public void OnEvent<TA, TB>()
    {
        OnEvents.Add(typeof(TA));
        OnEvents.Add(typeof(TB));
    }

    public void OnEvent<TA, TB, TC>()
    {
        OnEvents.Add(typeof(TA));
        OnEvents.Add(typeof(TB));
        OnEvents.Add(typeof(TC));
    }


    public void Execute(World world)
    {
        foreach (var type in OnEvents)
        {
            if (!world.HasEntityWith(type)) return;
        }

        foreach(var comp in world.Query<T1>())
        {
            Callback.Invoke(comp);
        }
    }
}
#endregion

#region T2
public class Subscribe<T1, T2> : ISubscribe
{
    private Action<T1, T2> Callback;
    private Query<T1, T2> query;
    private List<Type> OnEvents = new();


    public Subscribe(Action<T1, T2> callback)
    {
        Callback = callback;
    }


    public void BuildQuery(World world)
    {
        query = world.Query<T1, T2>();
        query.GetEnumerator();
    }

    public Query<T1, T2> Require<T>()
    {
        query.Require<T>();
        return query;
    }

    public Query<T1, T2> Exclude<T>()
    {
        query.Exclude<T>();
        return query;
    }

    
    public void OnEvent<TA>()
    {
        OnEvents.Add(typeof(TA));
    }

    public void OnEvent<TA, TB>()
    {
        OnEvents.Add(typeof(TA));
        OnEvents.Add(typeof(TB));
    }

    public void OnEvent<TA, TB, TC>()
    {
        OnEvents.Add(typeof(TA));
        OnEvents.Add(typeof(TB));
        OnEvents.Add(typeof(TC));
    }


    public void Execute(World world)
    {
        foreach (var type in OnEvents)
        {
            if (!world.HasEntityWith(type)) return;
        }

        foreach(var tuple in world.Query<T1, T2>())
        {
            Callback.Invoke(tuple.Item1, tuple.Item2);
        }
    }
}
#endregion
