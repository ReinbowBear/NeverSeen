using System;
using System.Collections.Generic;

public static class EventBusRegister
{
    public static readonly Dictionary<Type, Action<(Delegate, Priority)>> addDelegates = new();
    public static readonly Dictionary<Type, Action<(Delegate, Priority)>> removeDelegates = new();

    static EventBusRegister() // авто регестрация через рефлексию при сборке игры, добавление событий в отдельный файл? кароче автоматизируй ебалай
    {
        Register<OnSave>();
        Register<OnLoad>();

        Register<OnSceneEntry>();
        Register<OnSceneStart>();
        Register<OnSceneGameOver>();
        Register<OnSceneRelease>();

        Register<OnUpdateNetwork>();
    }

    public static void Register<T>()
    {
        var type = typeof(T);

        addDelegates[type] = pair => EventBus.Subscribe((Action<T>)pair.Item1, pair.Item2);
        removeDelegates[type] = pair => EventBus.Unsubscribe((Action<T>)pair.Item1);
    }
}


#region system
public class OnSave : EventArgs { }
public class OnLoad : EventArgs
{
    private Dictionary<string, SaveData> allData;

    public OnLoad(Dictionary<string, SaveData> newDict)
    {
        allData = newDict;
    }


    public bool TryGetDataById(string id, out SaveData data)
    {
        return allData.TryGetValue(id, out data);
    }
}
#endregion

#region Scene
public class OnSceneEntry : EventArgs { }
public class OnSceneStart : EventArgs { }
public class OnSceneGameOver : EventArgs { }
public class OnSceneRelease : EventArgs { }
#endregion

#region gamelay
public class OnUpdateNetwork : EventArgs { }
#endregion
