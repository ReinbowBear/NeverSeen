using UnityEngine;

public class PreviousTarget : BaseTarget
{
    private Entity previousEntity;
    public override Transform[] GetTarget(BattleMap batleMap, Entity character)
    {
        Debug.Log("PreviousTarget не готов!");
        Transform[] targets = new Transform[batleMap.points[!character.stats.isPlayer].Length];
//
        //if (batleMap.points[!mySide][0].childCount != 0)
        //{
        //    targets[0] = batleMap.points[!mySide][0];
        //}
        //
        return targets;
    }
}
