using UnityEngine;

public class SecondTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Transform[] targets = new Transform[batleMap.points[!character.currentStats.isPlayer].Length];

        if (batleMap.points[!character.currentStats.isPlayer][0].childCount != 0)
        {
            targets[0] = batleMap.points[!character.currentStats.isPlayer][0];
        }

        if (batleMap.points[!character.currentStats.isPlayer][1].childCount != 0)
        {
            targets[1] = batleMap.points[!character.currentStats.isPlayer][1];
            targets[0] = null;
        }
        
        return targets;
    }
}
