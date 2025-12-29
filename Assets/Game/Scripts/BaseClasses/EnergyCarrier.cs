using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnergyCarrier : MonoBehaviour, IHaveRadius
{
    public EventBus eventBus;
    public UnityEvent<bool> OnIsActive;
    public UnityEvent<Transform> OnConect;

    public byte Radius;
    public EnergyCarrierType type;
    public EnergyCarrierType connectionsType;

    [HideInInspector] public List<EnergyCarrier> connections = new();
    [HideInInspector] public bool isHasEnergy;

    public TileMap mapData;

    public virtual void Initialize()
    {
        RefreshConections();
    }


    public void RefreshConections()
    {
        connections.Clear();
        var myTile = mapData.GetTileFromCord(transform.position);

        foreach (var tile in mapData.GetTilesInRadius(myTile.tileData.CubeCoord, Radius))
        {
            tile.tileData.IsTaken.gameObject.TryGetComponent<EnergyCarrier>(out var component);

            if (component != null)
            {
                TryConnect(component);
            }
        }
        eventBus.Invoke<OnUpdateNetwork>();
    }

    protected void TryConnect(EnergyCarrier energyCarrier)
    {
        if ((energyCarrier.type & connectionsType) == 0) return; // 0 если нет совпадений по флагам

        connections.Add(energyCarrier);
        energyCarrier.connections.Add(this);

        OnConect.Invoke(energyCarrier.transform);
    }


    public virtual void SetActive(bool isActive)
    {
        isHasEnergy = isActive;
        OnIsActive.Invoke(isHasEnergy);
    }

    public virtual void TransferEnergy(EnergyTransferData transferData)
    {
        if (transferData.Visited.Contains(this)) return;

        transferData.Visited.Add(this);

        if (!isHasEnergy)
        {
            SetActive(true);
        }

        foreach (var neighbor in connections)
        {
            neighbor.TransferEnergy(transferData);
        }
    }


    public int GetRadius()
    {
        return Radius;
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
