using UnityEngine;

public class SecondTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, bool mySide)
    {
        Transform[] targets = new Transform[batleMap.points[!mySide].Length];

        if (batleMap.points[!mySide][0].childCount != 0)
        {
            targets[0] = batleMap.points[!mySide][0];
        }

        if (batleMap.points[!mySide][1].childCount != 0)
        {
            targets[1] = batleMap.points[!mySide][1];
            targets[0] = null;
        }
        
        return targets;
    }
}
