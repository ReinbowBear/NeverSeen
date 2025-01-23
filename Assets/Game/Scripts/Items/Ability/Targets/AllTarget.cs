using System.Collections.Generic;
using UnityEngine;

public class AllTarget : BaseTarget
{
    public override List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();

        for (byte i = 0; i < batleMap.points[!myCharacter.currentStats.isPlayer].Length; i++)
        {
            if (batleMap.points[!myCharacter.currentStats.isPlayer][i].childCount != 0)
            {
                targets.Add(batleMap.points[!myCharacter.currentStats.isPlayer][i]);
            }
        }
        
        return targets;
    }
}
