using System;
using UnityEngine;

#region global
public interface ISystem
{
    void SetFilter(Filter filter);
    void Update(World world, Filter filter);
}

public interface ISaveable<T> where T : struct
{
    T GetData();
    void SetData(T data);
}

public interface IStateMachine
{
    void SetState(string stateName);
}
public interface IState
{
    void Enter();
    void Exit();
    void Execute();
}
#endregion

#region Data
public interface IComponentData
{

}

public interface IHaveRadius
{
    int GetRadius();
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
#endregion


#region view
public interface IViewMode
{
    LayerMask GetRayLayer();
    void LeftClick(RaycastHit hit);
    void RightClick();
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

#region indicatorUI
public interface IHaveBar
{
    event Action<int, int> OnChangeValue;
}

public interface IHaveNumber
{
    event Action<int> OnChangeValue;
}
#endregion


#region utility

#endregion
