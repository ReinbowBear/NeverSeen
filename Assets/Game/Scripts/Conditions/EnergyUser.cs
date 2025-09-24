
public class EnergyUser : EnergyCarrier, ICondition
{
    private short usedEnergy;
    private IBehavior effect;

    public EnergyUser(short usedEnergy, IBehavior effect)
    {
        this.usedEnergy = usedEnergy;
        this.effect = effect;

        type = EnergyCarrierType.User;
        connectionsType = EnergyCarrierType.Generator | EnergyCarrierType.Tower;
    }

    public bool IsConditionMet()
    {
        return true;
    }

    public override void SetActive(bool isActive)
    {
        base.SetActive(isActive);
        effect.SetActive(isActive);
    }

    public override void TransferEnergy(EnergyTransferData energyData)
    {
        if (energyData.Visited.Contains(this)) return;
        if (energyData.Depth > energyData.MaxDepth) return;
        if (isHasEnergy) return;

        energyData.Visited.Add(this);
        isHasEnergy = energyData.Generator.GetEnergy(this, usedEnergy);
        SetActive(isHasEnergy);
    }
}
