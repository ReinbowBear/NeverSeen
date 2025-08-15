using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectoNetwork : EventSubscriber
{
    private List<EnergyCarrier> energyCarriers = new();
    private List<Generator> generators = new();


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

    [EventHandler]
    public void UpdateNetwork(OnUpdateNetwork onUpdate)
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
