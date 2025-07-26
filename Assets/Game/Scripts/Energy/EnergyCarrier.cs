using System.Collections.Generic;
using UnityEngine;

public abstract class EnergyCarrier : BuildingComponent
{
    public Transform wirePoint;
    [HideInInspector] public List<EnergyCarrier> connections = new();
    [HideInInspector] public bool isPowered;


    public override void Active()
    {
        foreach (var tile in owner.tilesInRadius)
        {
            if (tile.tileData.isTaken is Building building)
            {
                foreach (var component in building.components)
                {
                    if (component is EnergyCarrier energyCarrier)
                    {
                        connections.Add(energyCarrier);
                    }
                }
            }
        }
    }

    public virtual void TransferEnergy(HashSet<EnergyCarrier> visited, int depth, int maxDepth)
    {
        if (visited.Contains(this)) return;

        visited.Add(this);
        isPowered = true;

        foreach (var neighbor in connections)
        {
            neighbor.TransferEnergy(visited, depth + 1, maxDepth);
        }
    }
}
