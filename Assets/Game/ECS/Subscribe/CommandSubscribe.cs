using System;
using System.Collections.Generic;

#region T0
public class CommandSubscribe : ISubscribe
{
    private Action<EntityCommands> Callback;
    private List<Type> OnEvents = new();

    public CommandSubscribe(Action<EntityCommands> callback)
    {
        Callback = callback;
    }

    public void BuildQuery(World world)
    {

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

        var entityCommands = new EntityCommands(world, null); // нуль передавать опасненько, смотри не пытайся добавить компонент на ничто
        Callback.Invoke(entityCommands);
    }
}
#endregion

#region T1
public class CommandSubscribe<T1> : ISubscribe
{
    private Action<EntityCommands, T1> Callback;
    private Query<T1> query;
    private List<Type> OnEvents = new();

    public CommandSubscribe(Action<EntityCommands, T1> callback)
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


        var query = world.Query<T1>();
        var enumerator = query.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var entity = enumerator.CurrentEntity;
            var comp = enumerator.Current;

            var entityCommands = new EntityCommands(world, entity);

            Callback.Invoke(entityCommands, comp);
        }
    }
}
#endregion

#region T2
public class CommandSubscribe<T1, T2> : ISubscribe
{
    private Action<EntityCommands, T1, T2> Callback;
    private Query<T1, T2> query;
    private List<Type> OnEvents = new();

    public CommandSubscribe(Action<EntityCommands, T1, T2> callback)
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


        var query = world.Query<T1, T2>();
        var enumerator = query.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var entity = enumerator.CurrentEntity;
            var tulpe = enumerator.Current;

            var entityCommands = new EntityCommands(world, entity);

            Callback.Invoke(entityCommands, tulpe.Item1, tulpe.Item2);
        }
    }
}
#endregion