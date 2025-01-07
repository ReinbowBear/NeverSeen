using UnityEngine;

public class PreviousTarget : BaseTarget
{
    private Entity previousEntity;
    public override Transform[] GetTarget(BattleMap batleMap, bool mySide)
    {
        Debug.Log("PreviousTarget не готов!");
        Transform[] targets = new Transform[batleMap.points[!mySide].Length];
//
        //if (batleMap.points[!mySide][0].childCount != 0)
        //{
        //    targets[0] = batleMap.points[!mySide][0];
        //}
        //
        return targets;
    }
}
