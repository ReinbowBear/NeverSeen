using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class UpdateAfterAttribute : Attribute
{
    public Type TargetSystem;

    public UpdateAfterAttribute(Type targetSystem)
    {
        TargetSystem = targetSystem;
    }
}
