using UnityEngine;

public class LastTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Transform[] targets = new Transform[1];

        for (int i = batleMap.points[!character.currentStats.isPlayer].Length; i > 0; i--)
        {
            if (batleMap.points[!character.currentStats.isPlayer][i].childCount != 0)
            {
                targets[0] = batleMap.points[!character.currentStats.isPlayer][i];
            }
        }
        
        return targets;
    }
}
