using System.Collections.Generic;
using UnityEngine;

public class ElectoNetwork : MonoBehaviour
{
    public static ElectoNetwork Instance;

    private List<EnergyCarrier> energyCarriers = new();
    private List<Generator> generators = new();

    void Awake()
    {
        Instance = this;
    }

    public void AddEnergyCarrier(EnergyCarrier carrier)
    {
        if (carrier is Generator generator)
        {
            generators.Add(generator);
            return;
        }

        energyCarriers.Add(carrier);
    }

    public void RemoveEnergyCarrier(EnergyCarrier carrier)
    {
        if (carrier is Generator generator)
        {
            generators.Remove(generator);
            return;
        }

        energyCarriers.Remove(carrier);
    }


    public void UpdateNetwork()
    {
        foreach (var carrier in energyCarriers)
        {
            carrier.Active(false);
        }

        foreach (var generator in generators)
        {
            generator.Active(true);
        }
    }
}
