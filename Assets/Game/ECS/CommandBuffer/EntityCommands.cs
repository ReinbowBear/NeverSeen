using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityCommands
{
    private World world;
    private CommandBuffer buffer;
    private List<Type> events = new();
    public List<Type> Events => events;

    public EntityCommands(World world, CommandBuffer buffer)
    {
        this.world = world;
        this.buffer = buffer;
    }


    public void CreateEntity(GameObject obj)
    {
        var comand = new CreateEntityCommand(obj);
        buffer.OnStartFrame(comand);
    }

    public void DestroyEntity(Entity entity)
    {
        var comand = new DestroyEntityCommand(entity);
        buffer.OnStartFrame(comand);
    }


    public void AddOneFrame<T>(T comp) where T : struct
    {
        var entity = world.CreateEntity();
        buffer.OnStartFrame(new AddComponentCommand<T>(entity, comp));
        buffer.OnEndFrame(new RemoveComponentCommand<T>(entity));
        events.Add(typeof(T));
    }

    public void AddOneFrame<T>(Entity entity, T comp) where T : struct
    {
        buffer.OnStartFrame(new AddComponentCommand<T>(entity, comp));
        buffer.OnEndFrame(new RemoveComponentCommand<T>(entity));
        events.Add(typeof(T));
    }


    public void AddComponent<T>(Entity entity, T component)
    {
        var comand = new AddComponentCommand<T>(entity, component);
        buffer.OnStartFrame(comand);
    }

    public void RemoveComponent<T>(Entity entity)
    {
        var comand = new RemoveComponentCommand<T>(entity);
        buffer.OnStartFrame(comand);
    }


    public void Clear()
    {
        events.Clear();
    }
}
