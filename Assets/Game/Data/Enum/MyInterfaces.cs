using System.Collections.Generic;
using System.Threading.Tasks;

#region global
public interface IAsyncInit
{
    Task AsyncInit();
}

public interface ISaveable<T> where T : struct
{
    T CaptureData();
    void ApplyData(T data);
}
#endregion


#region entity
public interface ICondition
{
    bool IsConditionMet();
}

public interface IBehavior
{
    void SetActive(bool isActive);
}
#endregion

#region view
public interface IBarView
{
    void DrawBar(bool isActive);
    void ChangeValue(int value, int maxValue);
}

public interface IEnergyView
{
    void DrawWireTo(IEnergyView otherView);
    void ShowEnergyPoint(bool isActive);
}
#endregion


#region utility
public interface IInspectable
{
    string GetDisplayData();
}

public interface IConfig
{
    IBehavior Build();
}
#endregion
