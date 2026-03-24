using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class GameSystemAttribute : Attribute
{
    public readonly SystemGroup SystemGroup;
    public readonly UpdateState UpdateState;

    public GameSystemAttribute(SystemGroup systemGroup, UpdateState updateState = UpdateState.Global)
    {
        SystemGroup = systemGroup;
        UpdateState = updateState;
    }
}


public enum SystemGroup
{
    Enter,

    Init,
    Logic,
    View,

    Exit
}

[Flags]
public enum UpdateState
{
    Global,

    Menu,
    Loading,
    Gameplay,
    Pause
}
