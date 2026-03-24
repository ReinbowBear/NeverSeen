using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class UpdateBeforeAttribute : Attribute
{
    public Type TargetSystem;

    public UpdateBeforeAttribute(Type targetSystem)
    {
        TargetSystem = targetSystem;
    }
}
