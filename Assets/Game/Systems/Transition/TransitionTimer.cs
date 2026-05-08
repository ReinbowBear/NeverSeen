using UnityEngine;

public class TransitionTimer : ISystem
{
    public void Execute(World world, EntityCommands commands)
    {
        //world.AddWithCommands<TransitionInProgress>(CheckTime);
    }


    public void CheckTime(EntityCommands commands, TransitionInProgress transition)
    {
        transition.Time += Time.deltaTime;

        //if (transition.Time > transition.Duration)
        //{
        //    commands.AddComponent(new TransitionReady(transition.To));
        //    commands.RemoveComponent<TransitionInProgress>();
        //}
    }
}
