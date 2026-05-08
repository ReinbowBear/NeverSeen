using UnityEngine;

public class UIInputState : ISystem, IState
{
    private GameInput.UIActions actions;

    public UIInputState(GameInput.UIActions actions)
    {
        this.actions = actions;
    }


    public void Execute(World world, EntityCommands commands)
    {
        HandleNavigate(commands);
        HandleSumbit(commands);
        HandleEsc(commands);
    }


    private void HandleNavigate(EntityCommands commands)
    {
        if (actions.Navigate.WasPerformedThisFrame())
        {
            var direction = actions.Navigate.ReadValue<Vector2>();

            var intent = new IntentNavigate(direction);
            commands.AddOneFrame(intent);
        }
    }

    private void HandleSumbit(EntityCommands commands)
    {
        if (actions.Submit.IsPressed())
        {
            var intent = new IntentSumbit();
            commands.AddOneFrame(intent);
        }
    }

    private void HandleEsc(EntityCommands commands)
    {
        if (actions.Esc.IsPressed())
        {
            var intent = new IntentEsc();
            commands.AddOneFrame(intent);
        }
    }


    public void Enter()
    {
        actions.Enable();
    }

    public void Exit()
    {
        actions.Disable();
    }
}
