using UnityEngine;

public class BehaviorWrapper
{
    public IBehavior behavior;
    public ICondition condition;

    public BehaviorWrapper(IBehavior behavior, ICondition condition = null)
    {
        this.behavior = behavior;
        this.condition = condition;

        if (condition == null)
        {
            this.condition = new DefaultCondition();
        }
    }
}

public class DefaultCondition : ICondition
{
    public bool IsConditionMet()
    {
        return true;
    }
}