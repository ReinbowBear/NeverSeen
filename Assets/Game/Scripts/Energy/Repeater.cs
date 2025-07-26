using System.Collections.Generic;
using UnityEngine;

public class Repeater : EnergyCarrier
{
    [SerializeField] private byte EnergyBuff;


    public override void TransferEnergy(HashSet<EnergyCarrier> visited, int depth, int maxDepth)
    {
        if (depth > maxDepth || visited.Contains(this)) return;

        visited.Add(this);
        isPowered = true;

        foreach (var neighbor in connections)
        {
            neighbor.TransferEnergy(visited, -EnergyBuff, maxDepth);
        }
    }
}
