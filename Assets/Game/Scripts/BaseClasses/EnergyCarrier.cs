using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnergyCarrier : MonoBehaviour, IBehavior
{
    public EnergyCarrierType type { get; protected set; }
    public EnergyCarrierType connections { get; protected set; }

    public List<EnergyCarrier> connectionsList { get; protected set; }
    protected IEnergyView view;

    protected bool isHasEnergy;
    protected Entity owner;

    protected virtual void Init()
    {
        connectionsList = new();
        foreach (var tile in owner.TilesInRadius)
        {
            if (tile.tileData.IsTaken is Building building)
            {

                var carrierEffect = building.Behaviours.FirstOrDefault(effect => effect is EnergyCarrier);
                if (carrierEffect != null)
                {
                    TryConnect((EnergyCarrier)carrierEffect);
                }
            }
        }
        EventBus.Invoke<OnUpdateNetwork>();
    }


    public virtual void SetActive(bool isActive)
    {
        isHasEnergy = isActive;
        view.ShowEnergyPoint(isHasEnergy);
    }

    protected void TryConnect(EnergyCarrier energyCarrier)
    {
        if ((energyCarrier.type & connections) == 0) return; // 0 если нет совпадений по флагам

        connectionsList.Add(energyCarrier);
        energyCarrier.connectionsList.Add(this);

        view.ShowEnergyPoint(true);
        view.DrawWireTo(energyCarrier.view);
    }

    public virtual void TransferEnergy(EnergyData energyData)
    {
        if (energyData.Visited.Contains(this)) return;
        if (energyData.Depth > energyData.MaxDepth) return;
        if (isHasEnergy) return;

        energyData.Visited.Add(this);
        SetActive(true);

        energyData.Depth += 1;
        foreach (var neighborKey in connectionsList)
        {
            neighborKey.TransferEnergy(energyData);
        }
    }
}


[Flags]
public enum EnergyCarrierType
{
    None = 0,
    Generator = 1,
    Tower = 2,
    User = 4
}
