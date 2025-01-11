using UnityEngine;

public class AllTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Transform[] targets = new Transform[batleMap.points[!character.stats.isPlayer].Length];

        for (byte i = 0; i < batleMap.points[!character.stats.isPlayer].Length; i++)
        {
            if (batleMap.points[!character.stats.isPlayer][i].childCount != 0)
            {
                targets[i] = batleMap.points[!character.stats.isPlayer][i];
            }
        }
        
        return targets;
    }
}
