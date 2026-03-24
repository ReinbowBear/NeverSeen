using System;
using System.Collections.Generic;

public class StateMachine<TKey, TState> : IDisposable where TState : IState
{
    private Dictionary<TKey, IState> states = new();
    public TState CurrentState { get; private set; }

    public IState this[TKey key]
    {
        get => states[key];
        set => states[key] = value;
    }


    public void AddState(TKey key, IState state)
    {
        states[key] = state;
    }

    public void RemoveState(TKey key)
    {
        states.Remove(key);
    }


    public void SetActive(bool isActive)
    {
        if (CurrentState == null) return;

        if (isActive) CurrentState.Enter();
        else CurrentState.Exit();
    }

    public TState GetState(TKey key)
    {
        return (TState)states[key];
    }



    public void SetState(TKey key)
    {
        CurrentState?.Exit();
        CurrentState = (TState)states[key];
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
