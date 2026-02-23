using UnityEngine;

public struct Quest : ICondition
{
    public string Title;
    public string Description;

    private short requiredValue;
    private short currentValue;


    public string GetProgress()
    {
        return $"{Mathf.Min(currentValue, requiredValue)} / {requiredValue}";
    }

    public bool IsConditionMet()
    {
        return true;
    }
}
