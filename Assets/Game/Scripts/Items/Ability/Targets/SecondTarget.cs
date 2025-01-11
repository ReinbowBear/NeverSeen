using UnityEngine;

public class SecondTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Transform[] targets = new Transform[batleMap.points[!character.stats.isPlayer].Length];

        if (batleMap.points[!character.stats.isPlayer][0].childCount != 0)
        {
            targets[0] = batleMap.points[!character.stats.isPlayer][0];
        }

        if (batleMap.points[!character.stats.isPlayer][1].childCount != 0)
        {
            targets[1] = batleMap.points[!character.stats.isPlayer][1];
            targets[0] = null;
        }
        
        return targets;
    }
}
