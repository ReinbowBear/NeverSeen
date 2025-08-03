
public interface IInspectable
{
    string GetDisplayData();
}

public interface IBuildingCondition
{
    bool IsConditionMet();
}

public interface IBuildingEffect
{
    void OnActivate();
    void OnDeactivate();
}
