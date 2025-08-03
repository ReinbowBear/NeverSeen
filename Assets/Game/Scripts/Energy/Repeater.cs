using UnityEngine;

public class Repeater : EnergyCarrier
{
    [SerializeField] private byte EnergyBuff;

    public override void TransferEnergy(EnergyData energyData)
    {
        if (energyData.Visited.Contains(this) || energyData.Depth > energyData.MaxDepth) return;

        energyData.Visited.Add(this);
        EnergyPoint.SetActive(true);

        energyData.Depth = -EnergyBuff;
        foreach (var neighborKey in ConnectionsList.Keys)
        {
            neighborKey.TransferEnergy(energyData);
        }
    }
}
