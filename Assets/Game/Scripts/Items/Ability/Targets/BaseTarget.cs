using System.Collections.Generic;
using UnityEngine;

public class BaseTarget
{
    public virtual Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        List<Transform> targets = new List<Transform>();

        if (batleMap.points[!character.currentStats.isPlayer][0].childCount != 0)
        {
            targets.Add(batleMap.points[!character.currentStats.isPlayer][0]);
        }
        
        return targets.ToArray();
    }
}
