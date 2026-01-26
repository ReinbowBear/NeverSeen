
public interface ICondition
{
    bool IsConditionMet();
}

public interface IBehavior
{
    void SetActive(bool isActive);
}

public interface IHaveRadius
{
    int GetRadius();
}