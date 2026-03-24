using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRunner
{
    private StateMachine<UpdateState, SceneContext> stateMachine = new();
    private SceneContext CurrentContext => stateMachine.CurrentState;


    public void AddContext(UpdateState updateState, SceneContext context)
    {
        stateMachine.AddState(updateState ,context);
    }


    public void UpdateWorld()
    {
        foreach(var system in CurrentContext.Systems)
        {
            system.UpdateSystem(CurrentContext.World);
        }
    }


    public void SetState(UpdateState updateState)
    {
        CurrentContext.EventWorld.Invoke(Events.SceneEvents.CloseScene);
        stateMachine.SetState(updateState);
        CurrentContext.EventWorld.Invoke(Events.SceneEvents.EnterScene);
    }
}


public class SceneContext : IState
{
    public Container Container = new();
    public List<ISystem> Systems = new();
    public List<IEventListener> Listeners = new();

    public EventWorld EventWorld = new();
    public World World = new();

    public SceneContext(IEnumerable<object> objects)
    {
        foreach (var item in objects)
        {
            if(item is ISystem system) Systems.Add(system);
            if(item is IEventListener listener)
            {
                listener.SetEvents(EventWorld);
                Listeners.Add(listener);
            }
        }
    }


    public void Enter()
    {
        foreach(var system in Systems)
        {
            Container.Resolve(system);
        }

        foreach (var sender in MonoBehaviour.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IEventSender>())
        {
            sender.SetEventBus(EventWorld);
        }
    }

    public void Exit()
    {
        Container.Dispose();
        Container.Clear();
        World.Clear();
    }
}
