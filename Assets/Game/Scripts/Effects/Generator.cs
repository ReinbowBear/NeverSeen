using System.Collections.Generic;

public class Generator : EnergyCarrier
{
    private IBarView barView;
    private List<EnergyUser> generatorUsers = new();

    private short maxEnergy;
    private short energy;
    private short range;

    public Generator(short maxEnergy, short range, IBarView barView)
    {
        this.maxEnergy = maxEnergy;
        this.range = range;
        this.barView = barView;

        energy = maxEnergy;
    }

    protected override void Init()
    {
        base.Init();
        barView.ChangeValue(energy, maxEnergy);
    }

    public override void SetActive(bool isActive)
    {
        base.Init();
        TransferEnergy(new EnergyData(this, range));

        barView.ChangeValue(energy, maxEnergy);
        barView.DrawBar(false);
    }


    public bool GetEnergy(EnergyUser newUser, short usedEnergy)
    {
        if (energy < usedEnergy) return false;

        generatorUsers.Add(newUser);
        energy -= usedEnergy;
        return true;
    }

    public void ReleaseEnergy(EnergyUser oldUser, short usedEnergy)
    {
        if (!generatorUsers.Contains(oldUser)) return;

        generatorUsers.Remove(oldUser);
        energy += usedEnergy;
    }


    public void Selected(bool isSelected) // надо подписать на выделение
    {
        //rotateBar.enabled = isSelected;
        //energyBar.FadeBar(isSelected ? 1f : 0f);
    }
}


public struct EnergyData
{
    public HashSet<EnergyCarrier> Visited;
    public int Depth;
    public int MaxDepth;
    public Generator Generator;

    public EnergyData(Generator newGenerator, int newMaxDepth)
    {
        Visited = new();
        Depth = 0;

        MaxDepth = newMaxDepth;
        Generator = newGenerator;

        Visited.Add(newGenerator);
    }
}
