
public interface IInspectable
{
    string GetDisplayData();
}

public interface ICondition
{
    bool IsConditionMet();
}

public interface IBuildingEffect
{
    void Activate();
    void Deactivate();
}
