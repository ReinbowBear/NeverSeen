
public class EnergyUser : EnergyCarrier, ICondition
{
    public short usedEnergy;

    public bool IsConditionMet()
    {
        return isHasEnergy;
    }

    public override void TransferEnergy(EnergyTransferData transferData)
    {
        if (transferData.Visited.Contains(this)) return;
        if (transferData.EnergyLeft! >= usedEnergy) return;

        transferData.Visited.Add(this);
        transferData.EnergyLeft -= usedEnergy;
        SetActive(true);

        foreach (var neighbor in connections)
        {
            neighbor.TransferEnergy(transferData);
        }
    }
}
