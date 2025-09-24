using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IState> states = new();
    public IState CurrentState { get; private set; }

    public void Start<T>() where T : class, IState
    {
        var type = typeof(T);

        CurrentState = states[type];
        CurrentState.Enter();
    }


    public void SetMode<T>() where T : class, IState
    {
        var type = typeof(T);

        CurrentState.Exit();
        CurrentState = states[type];
        CurrentState.Enter();
    }


    public void AddState(IState newState)
    {
        states.Add(newState.GetType(), newState);
    }

    public void RemoveState(IState newState)
    {
        states.Remove(newState.GetType());
    }
    
    public T GetState<T>() where T : class, IState
    {
        var type = typeof(T);
        return (T)states[type];
    }
}
