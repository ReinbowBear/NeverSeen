using System;
using System.Linq;

public class Callback : IComponentCallback
{
    private Action callback;

    public Callback(Action callback)
    {
        this.callback = callback;
    }

    public void TryInvoke(object[] args)
    {
        callback.Invoke();
    }
}

public class Callback<T1> : IComponentCallback
{
    private Action<T1> callback;

    public Callback(Action<T1> callback)
    {
        this.callback = callback;
    }

    public void TryInvoke(object[] args)
    {
        var comp1 = args.OfType<T1>().FirstOrDefault();
        if (comp1 == null) return;

        callback.Invoke(comp1);
    }
}

public class Callback<T1, T2> : IComponentCallback
{
    private Action<T1, T2> callback;

    public Callback(Action<T1, T2> callback)
    {
        this.callback = callback;
    }


    public void TryInvoke(object[] args)
    {
        var comp1 = args.OfType<T1>().FirstOrDefault();
        if (comp1 == null) return;

        var comp2 = args.OfType<T2>().FirstOrDefault();
        if (comp2 == null) return;

        callback.Invoke(comp1, comp2);
    }
}

public class Callback<T1, T2, T3> : IComponentCallback
{
    private Action<T1, T2, T3> callback;

    public Callback(Action<T1, T2, T3> callback)
    {
        this.callback = callback;
    }


    public void TryInvoke(object[] args)
    {
        var comp1 = args.OfType<T1>().FirstOrDefault();
        if (comp1 == null) return;

        var comp2 = args.OfType<T2>().FirstOrDefault();
        if (comp2 == null) return;

        var comp3 = args.OfType<T3>().FirstOrDefault();
        if (comp3 == null) return;

        callback.Invoke(comp1, comp2, comp3);
    }
}
