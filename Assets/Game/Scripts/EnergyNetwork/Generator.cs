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

    public override void Init()
    {
        //base.Init(); // а как он конектится то
        currentEnergy = energyValue;
        energyBar.StartedValue(currentEnergy);
    }

    public override void Active(bool isActive)
    {
        base.Init();
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


    public bool GetEnergy(EnergyUser newUser, byte usedEnergy)
    {
        if (currentEnergy < usedEnergy) return false;

        generatorUsers.Add(newUser);
        currentEnergy -= usedEnergy;
        return true;
    }

    public void ReleaseEnergy(EnergyUser oldUser, byte usedEnergy)
    {
        if (!generatorUsers.Contains(oldUser)) return;

        generatorUsers.Remove(oldUser);
        currentEnergy += usedEnergy;
    }


    public void Selected(bool isSelected) // надо подписать на выделение
    {
        rotateBar.enabled = isSelected;
        energyBar.FadeBar(isSelected ? 1f : 0f);
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
