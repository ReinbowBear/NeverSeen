using System.Collections.Generic;
using UnityEngine;

public class EnergyUser : EnergyCarrier
{
    public override void TransferEnergy(HashSet<EnergyCarrier> visited, int depth, int maxDepth)
    {
        if (depth > maxDepth || visited.Contains(this)) return;

        visited.Add(this);
        isPowered = true;

        // жрёт энергию
    }
}
