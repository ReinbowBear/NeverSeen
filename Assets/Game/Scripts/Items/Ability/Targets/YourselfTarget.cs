using System.Collections.Generic;
using UnityEngine;

public class YourselfTarget : BaseTarget
{
    public override List<Transform> GetTarget(BattleMap batleMap, Entity myCharacter)
    {
        List<Transform> targets = new List<Transform>();
        targets.Add(myCharacter.transform);
        
        return targets;
    }
}
