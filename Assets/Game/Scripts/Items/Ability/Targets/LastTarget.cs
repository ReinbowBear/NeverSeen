using System.Collections.Generic;
using UnityEngine;

public class LastTarget : BaseTarget
{
    public override List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();

        for (int i = batleMap.points[!myCharacter.currentStats.isPlayer].Length; i > 0; i--)
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
