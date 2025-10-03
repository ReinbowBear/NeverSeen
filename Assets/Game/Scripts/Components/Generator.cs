using System.Collections.Generic;
using UnityEngine.Events;

public class Generator : EnergyCarrier
{
    public UnityEvent<float, float> OnValueChanged;

    public int maxEnergy;
    public int CurrentEnergy;

    public HashSet<EnergyCarrier> allConections { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        OnValueChanged.Invoke(maxEnergy, maxEnergy);
    }

    public override void SetActive(bool isActive) // не умеет выключать
    {
        var energyData = new EnergyTransferData(maxEnergy);
        TransferEnergy(energyData);

        CurrentEnergy = energyData.EnergyLeft;
        allConections = energyData.Visited;
        OnValueChanged.Invoke(CurrentEnergy, maxEnergy);
    }
}

public class EnergyTransferData
{
    public HashSet<EnergyCarrier> Visited;
    public int EnergyLeft;

    public EnergyTransferData(int maxEnergy)
    {
        Visited = new();
        EnergyLeft = maxEnergy;
    }
}
