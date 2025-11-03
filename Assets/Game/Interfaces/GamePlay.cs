
public interface ICondition
{
    bool IsConditionMet();
}

public interface IBehavior
{
    void SetActive(bool isActive);
}


public interface IEnergyCarrier
{

}

public interface IGenerator
{

}

public interface IMiner
{

}

public interface IStorage
{

}

public interface IHaveRadius
{
    int GetRadius();
}