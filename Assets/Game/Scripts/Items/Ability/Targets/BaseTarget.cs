using System.Collections.Generic;
using UnityEngine;

public class BaseTarget
{
    public virtual List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();

        if (batleMap.points[!myCharacter.currentStats.isPlayer][0].childCount != 0)
        {
            targets.Add(batleMap.points[!myCharacter.currentStats.isPlayer][0]);
        }
        
        return targets;
    }
}
