
public class EnergyUser : EnergyCarrier
{
    private short usedEnergy;
    private IBehavior effect;

    public EnergyUser(short usedEnergy, IBehavior effect)
    {
        this.usedEnergy = usedEnergy;
        this.effect = effect;

        type = EnergyCarrierType.User;
        connections = EnergyCarrierType.Generator | EnergyCarrierType.Tower;
    }

    public override void SetActive(bool isActive)
    {
        base.SetActive(isActive);
        effect.SetActive(isActive);
    }

    public override void TransferEnergy(EnergyData energyData)
    {
        if (energyData.Visited.Contains(this)) return;
        if (energyData.Depth > energyData.MaxDepth) return;
        if (isHasEnergy) return;

        energyData.Visited.Add(this);
        isHasEnergy = energyData.Generator.GetEnergy(this, usedEnergy);
        SetActive(isHasEnergy);
    }
}
