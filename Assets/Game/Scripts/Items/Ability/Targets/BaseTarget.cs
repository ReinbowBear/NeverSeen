using UnityEngine;

public class BaseTarget
{
    public virtual Transform[] GetTarget(BattleMap batleMap, bool mySide)
    {
        Transform[] targets = new Transform[1];

        if (batleMap.points[!mySide][0].childCount != 0)
        {
            targets[0] = batleMap.points[!mySide][0];
        }
        
        return targets;
    }
}
