using System.Collections.Generic;
using UnityEngine;

public class Generator : EnergyCarrier
{
    [Space]
    [SerializeField] private byte energyValue;
    [SerializeField] private byte energyRange;

    private byte currentEnergy;

    public override void Active()
    {
        base.Active();
        currentEnergy = energyValue;
    }


    public void CheckEnergy()
    {
        HashSet<EnergyCarrier> visited = new();
        base.TransferEnergy(visited, 0, energyRange);
    }
}
