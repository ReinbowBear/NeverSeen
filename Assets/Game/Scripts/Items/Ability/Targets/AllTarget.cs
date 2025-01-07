using UnityEngine;

public class AllTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, bool mySide)
    {
        Transform[] targets = new Transform[batleMap.points[!mySide].Length];

        for (byte i = 0; i < batleMap.points[!mySide].Length; i++)
        {
            if (batleMap.points[!mySide][i].childCount != 0)
            {
                targets[i] = batleMap.points[!mySide][i];
            }
        }
        
        return targets;
    }
}
