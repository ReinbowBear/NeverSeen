using System;
using UnityEngine;

#region global
public interface ISaveable<T> where T : struct
{
    T CaptureData();
    void ApplyData(T data);
}

public interface IState
{
    void Enter();
    void Exit();
}
#endregion


#region UI
public interface IPanel
{
    void SetActive(bool isActive);
    void SetNavigation(bool isEnable);
}

public interface IBarView
{
    void DrawBar(bool isActive);
    void ChangeValue(int value, int maxValue);
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
public interface IViewMode
{
    LayerMask GetRayLayer();
    void LeftClick(RaycastHit hit);
    void RightClick();
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
    object[] GetArgs();
}

#endregion
