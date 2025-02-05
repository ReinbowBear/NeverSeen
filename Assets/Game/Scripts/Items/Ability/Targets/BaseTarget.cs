using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target", menuName = "ScriptableObject/Targets/BaseTarget")]
public class BaseTarget : TargetSO
{
    public bool isEntitySide;
    public bool allTarget;
    public Vector2 allowedPos;
    public Vector2 attackDist;


    public override List<Transform> GetTarget(Entity character)
    {
        BattleMap batleMap = BattleMap.instance;
        List<Transform> targets = new List<Transform>();
        bool side = !character.baseStats.isPlayer;

        if (isEntitySide)
        {
            side = character.baseStats.isPlayer;
        }


        for (int i = (int)attackDist.x; i < attackDist.y; i++)
        {
            if (batleMap.points[side][i].childCount != 0)
            {
                targets.Add(batleMap.points[side][i]);
                if (allTarget == false)
                {
                    break;
                }
            }
        }
        return targets;
    }
}
