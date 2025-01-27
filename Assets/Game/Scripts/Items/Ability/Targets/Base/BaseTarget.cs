using System.Collections.Generic;
using UnityEngine;

public class BaseTarget
{
    public TargetSO stats;

    public virtual List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();

        for (byte i = stats.midDist; i < stats.maxDist; i++)
        {
            if (batleMap.points[!myCharacter.currentStats.isPlayer][i].childCount != 0)
            {
                targets.Add(batleMap.points[!myCharacter.currentStats.isPlayer][i]);

                break;
            }
        }

        return targets;
    }
}
