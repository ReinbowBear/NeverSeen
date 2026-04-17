using UnityEngine;

public class FindEntity : ISystem
{
    public void SetSubs(SystemSubs subs)
    {
        subs.AddWithCommands(Find).OnEvent<OnSceneEnter>();
    }


    public void Find(EntityCommands commands)
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Entity"))
        {
            commands.CreateEntity(obj);
        }
    }
}
