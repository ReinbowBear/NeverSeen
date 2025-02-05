using System.Collections.Generic;
using UnityEngine;

public class TargetSO : ScriptableObject
{
    public virtual List<Transform> GetTarget(Entity character)
    {
        return null;
    }
}
