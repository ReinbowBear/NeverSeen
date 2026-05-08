using System.Collections.Generic;
using UnityEngine;

public class WorldRunner
{
    private List<SystemGroup> groups;
    private World world;
    private CommandBuffer commandBuffer;
    private EntityCommands commands;

    public SystemGroup LastGroup => groups[groups.Count - 1];
    public World World => world;
    public CommandBuffer Buffer => commandBuffer;
    public EntityCommands Commands => commands;

    public WorldRunner()
    {
        groups = new();
        world = new();
        commandBuffer = new();
        commands = new(world, commandBuffer);
    }


    public void AddGroup(SystemGroup systemGroup) // нет защиты от двойного исполнения систем, если те дублируются в групах
    {
        groups.Add(systemGroup);
    }

    public void RemoveGroup(SystemGroup systemGroup)
    {
        groups.Remove(systemGroup);
    }


    public void Update()
    {
        RunPhase(Phase.Triger);
        commandBuffer.ExecuteCommands(world);
    
        RunPhase(Phase.Valid);
        commandBuffer.ExecuteCommands(world);

        RunPhase(Phase.Logic);
        RunPhase(Phase.View);
        commandBuffer.ExecuteAfterFrame(world);
        commands.Clear();
    }


    private void RunPhase(Phase phase)
    {
        foreach (var group in groups)
        {
            group.Update(phase, world, commands);
        }
    }


    public void Clear()
    {
        groups.Clear();
        world.Clear();
        commandBuffer.Clear();
    }
}
