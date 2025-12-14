using UnityEngine;

public interface IToggle
{
    void Toggle();
}

public interface IViewMode
{
    LayerMask GetRayLayer();
    void LeftClick(RaycastHit hit);
    void RightClick();
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