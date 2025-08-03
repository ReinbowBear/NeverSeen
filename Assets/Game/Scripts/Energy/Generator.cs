using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : EnergyCarrier
{
    [Space]
    [SerializeField] private MyBar energyBar;
    [SerializeField] private RotateToCam rotateBar;
    [Space]
    [SerializeField] private byte energyValue;
    [SerializeField] private byte energyRange;

    private List<EnergyUser> generatorUsers = new();
    private byte currentEnergy;

    public override void Active()
    {
        currentEnergy = energyValue;
        energyBar.StartedValue(currentEnergy);
        StartCoroutine(UpdateEnergyBar());
        base.Active();
    }


    public bool GetEnergy(EnergyUser newUser, byte usedEnergy)
    {
        if (currentEnergy >= usedEnergy)
        {
            generatorUsers.Add(newUser);
            currentEnergy -= usedEnergy;
            return true;
        }
        return false;
    }

    public void ReleaseEnergy(EnergyUser oldUser, byte usedEnergy)
    {
        generatorUsers.Remove(oldUser);
        currentEnergy += usedEnergy;
    }


    public void CheckEnergy()
    {
        TransferEnergy(new EnergyData(this, energyRange));
        StartCoroutine(UpdateEnergyBar());
    }

    private IEnumerator UpdateEnergyBar()
    {
        rotateBar.enabled = true;

        energyBar.SetValue(currentEnergy, energyValue);
        energyBar.FadeBar(0);

        yield return energyBar.fadeCoroutine;
        rotateBar.enabled = false;
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
    }
}
