using System;
using System.Collections.Generic;

public class CommandBus
{
    private Dictionary<Type, List<Action<object>>> handlers = new();

    public CommandToken Register<T>(Action<T> handler)
    {
        var wrapper = new Action<object>(obj => handler((T)obj));

        if (!handlers.TryGetValue(typeof(T), out var list))
        {
            handlers[typeof(T)] = list = new();
        }

        list.Add(wrapper);

        return new CommandToken
        {
            Type = typeof(T),
            Handler = wrapper
        };
    }

    public void Unregister(CommandToken sub)
    {
        if (handlers.TryGetValue(sub.Type, out var list))
        {
            list.Remove(sub.Handler);
        }
    }


    public void Send<T>(T command)
    {
        if (handlers.TryGetValue(typeof(T), out var list))
        {
            foreach (var handler in list)
            {
                handler(command);
            }
        }
    }
}


public class CommandToken
{
    public Type Type;
    public Action<object> Handler;
}
