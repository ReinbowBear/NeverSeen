using System;
using System.Collections.Generic;

public class StateMachine : IDisposable
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


    public void SetActive(bool isActive)
    {
        if (isActive) CurrentState.Enter();
        else CurrentState.Exit();
    }

    public void SetMode(IState newState)
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

    public void Dispose()
    {
        CurrentState?.Exit();
    }
}

public interface IState
{
    void Enter();
    void Exit();
}
