using System;
using System.Collections.Generic;

public class EntityCommandBuffer
{
    private List<Command> commands = new();

    public void Playback()
    {
        foreach (var command in commands)
        {
            command.Action.Invoke();
        }

        commands.Clear();
    }

    public void Add(Command command)
    {
        commands.Add(command);
    }
}

public struct Command
{
    public readonly Action Action;

    public Command(Action action)
    {
        Action = action;
    }
}
