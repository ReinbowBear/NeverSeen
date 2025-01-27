using System.Collections.Generic;
using UnityEngine;

public class FromPosTarget : BaseTarget
{
    public override List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();

        for (int i = myCharacter.move.pos + stats.midDist; i < stats.maxDist; i++)
        {
            if (batleMap.points[myCharacter.currentStats.isPlayer][i].childCount != 0)
            {
                targets.Add(batleMap.points[!myCharacter.currentStats.isPlayer][i]);
            }
        }
        
        return targets;
    }
}
