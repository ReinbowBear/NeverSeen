using System.Collections.Generic;

public class CommandBuffer
{
    private List<ICommand> commandsBefore = new();
    private List<ICommand> commandsAfter = new();


    public void OnStartFrame(ICommand command)
    {
        commandsBefore.Add(command);
    }

    public void OnEndFrame(ICommand command)
    {
        commandsAfter.Add(command);
    }


    public void ExecuteCommands(World world)
    {
        foreach (var command in commandsBefore)
        {
            command.Execute(world);
        }

        commandsBefore.Clear();
    }

    public void ExecuteAfterFrame(World world)
    {
        foreach (var command in commandsAfter)
        {
            command.Execute(world);
        }

        commandsAfter.Clear();
    }


    public void Clear()
    {
        commandsBefore.Clear();
        commandsAfter.Clear();
    }
}

public interface ICommand
{
    void Execute(World world);
}
