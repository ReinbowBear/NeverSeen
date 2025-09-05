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

        Register<OnPanelOpen>();

        Register<OnSound>();
        Register<OnSceneStart>();
        Register<OnSceneRelease>();

        Register<OnUpdateNetwork>();
        Register<OnGameOver>();
    }

    public static void Register<T>()
    {
        var type = typeof(T);

        addDelegates[type] = pair => EventBus.AddSubscriber((Action<T>)pair.Item1, pair.Item2);
        removeDelegates[type] = pair => EventBus.RemoveSubscriber((Action<T>)pair.Item1);
    }
}


#region system
public class OnSave : EventArgs { }
public class OnLoad : EventArgs { }
#endregion

#region UI
public class OnPanelOpen : EventArgs
{
    public Panel panel;
    public bool isOpen;

    public OnPanelOpen(Panel panel, bool isOpen)
    {
        this.panel = panel;
        this.isOpen = isOpen;
    }
}
#endregion

#region Scene
public class OnSceneStart : EventArgs { }
public class OnSceneRelease : EventArgs { }
#endregion

#region gamelay
public struct OnSound
{
    public SoundData SoundData;

    public OnSound(SoundData soundData)
    {
        SoundData = soundData;
    }
}
public class OnUpdateNetwork : EventArgs { }
public class OnGameOver : EventArgs { }
#endregion
