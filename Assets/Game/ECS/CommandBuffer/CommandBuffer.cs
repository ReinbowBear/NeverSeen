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


    public void ExecuteCommands()
    {
        foreach (var command in commandsBefore) command.Execute();
        commandsBefore.Clear();
    }

    public void ExecuteAfterFrame()
    {
        foreach (var command in commandsAfter) command.Execute();
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
    void Execute();    
}
