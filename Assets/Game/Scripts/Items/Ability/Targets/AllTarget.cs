using UnityEngine;

public class AllTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Transform[] targets = new Transform[batleMap.points[!character.currentStats.isPlayer].Length];

        for (byte i = 0; i < batleMap.points[!character.currentStats.isPlayer].Length; i++)
        {
            if (batleMap.points[!character.currentStats.isPlayer][i].childCount != 0)
            {
                targets[i] = batleMap.points[!character.currentStats.isPlayer][i];
            }
        }
        
        return targets;
    }
}
