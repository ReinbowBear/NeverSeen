using UnityEngine;

public class LastTarget : BaseTarget
{
    public override Transform[] GetTarget(BattleMap batleMap, bool mySide)
    {
        Transform[] targets = new Transform[1];

        for (int i = batleMap.points[!mySide].Length; i > 0; i--)
        {
            if (batleMap.points[!mySide][i].childCount != 0)
            {
                targets[0] = batleMap.points[!mySide][i];
            }
        }
        
        return targets;
    }
}
