using System.Collections.Generic;
using UnityEngine;

public class SecondTarget : BaseTarget
{
    public override List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();

        if (batleMap.points[!myCharacter.currentStats.isPlayer][0].childCount != 0)
        {
            targets.Add(batleMap.points[!myCharacter.currentStats.isPlayer][0]);
        }

        if (batleMap.points[!myCharacter.currentStats.isPlayer][1].childCount != 0)
        {
            targets.RemoveAt(0);
            targets.Add(batleMap.points[!myCharacter.currentStats.isPlayer][1]);
        }
        
        return targets;
    }
}
