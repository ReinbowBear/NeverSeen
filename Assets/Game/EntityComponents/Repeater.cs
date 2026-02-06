using UnityEngine;

public class Repeater : EnergyCarrier
{
    [SerializeField] private byte EnergyBuff;

    public override void TransferEnergy(EnergyTransferData energyData)
    {
        if (energyData.Visited.Contains(this)) return;
        //if (energyData.Depth > energyData.MaxDepth) return;
        if (isHasEnergy) return;

        energyData.Visited.Add(this);
        SetActive(true);

        //energyData.Depth = -EnergyBuff;
        foreach (var neighborKey in connections)
        {
            neighborKey.TransferEnergy(energyData);
        }
    }
}
