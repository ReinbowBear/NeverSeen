using UnityEngine;

public class EnergyUser : EnergyCarrier, ICondition
{
    [SerializeField] private byte UsedEnergy;
    private Generator generator;

    public override void Active(bool isActive)
    {
        base.Active(isActive);

        if (isActive == false && generator != null)
        {
            generator.ReleaseEnergy(this, UsedEnergy);
        }
    }

    public override void Delete()
    {
        if (generator != null)
        {
            generator.ReleaseEnergy(this, UsedEnergy);
        }
        base.Delete();
    }

    public bool IsConditionMet()
    {
        return EnergyPoint.activeSelf;
    }

    public override void TransferEnergy(EnergyData energyData)
    {        
        if (energyData.Visited.Contains(this)) return;
        if (energyData.Depth > energyData.MaxDepth) { EnergyPoint.SetActive(false); return; }

        energyData.Visited.Add(this);
        EnergyPoint.SetActive(true);

        generator = energyData.Generator;
        EnergyPoint.SetActive(generator.GetEnergy(this, UsedEnergy));

        Owner.Active(true);
    }
}
