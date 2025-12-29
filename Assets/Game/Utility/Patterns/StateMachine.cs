using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IState> states = new();
    public IState CurrentState { get; private set; }


    public void AddState(IState newState)
    {
        var type = newState.GetType();
        states[type] = newState;
    }

    public void RemoveState(IState oldState)
    {
        var type = oldState.GetType();
        states.Remove(type);
    }


    public void SetCurrent(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = states[newState.GetType()];
        CurrentState.Enter();
    }

    public void SetMode<T>() where T : class, IState
    {
        var type = typeof(T);

        CurrentState?.Exit();
        CurrentState = states[type];
        CurrentState.Enter();
    }

    public void Exit()
    {
        CurrentState?.Exit();
    }
}

public interface IState
{
    void Enter();
    void Exit();
}
