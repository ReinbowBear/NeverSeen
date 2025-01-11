using UnityEngine;

public class BaseTarget
{
    public virtual Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Transform[] targets = new Transform[1];

        if (batleMap.points[!character.stats.isPlayer][0].childCount != 0)
        {
            targets[0] = batleMap.points[!character.stats.isPlayer][0];
        }
        
        return targets;
    }
}
