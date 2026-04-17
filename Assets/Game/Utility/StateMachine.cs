using System;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<TKey, TState> : IDisposable, IEnumerable<TState> where TState : IState
{
    private Dictionary<TKey, IState> states = new();
    public TState CurrentState { get; private set; }

    public TState this[TKey key]
    {
        get => (TState)states[key];
        set => states[key] = value;
    }

    public IEnumerator<TState> GetEnumerator()
    {
        foreach (var state in states.Values)
        {
            yield return (TState)state;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
