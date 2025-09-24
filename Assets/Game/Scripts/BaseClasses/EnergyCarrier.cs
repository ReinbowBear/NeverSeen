using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class EnergyCarrier : MonoBehaviour
{
    public EnergyCarrierType type;
    public EnergyCarrierType connectionsType;
    [HideInInspector] public List<EnergyCarrier> connections = new();
    [Space]
    [HideInInspector] public bool isHasEnergy;
    [Space]
    public UnityEvent<bool> OnIsActive;
    public UnityEvent<Transform> OnConect;
    [Inject] TileMapData mapData;

    private Entity entity;

    protected virtual void Init()
    {
        entity = GetComponent<Entity>();
        var myTile = mapData.GetTileFromCord(transform.position);

        foreach (var tile in mapData.GetTilesInRadius(myTile.tileData.CubeCoord, entity.Stats.Radius))
        {
            tile.tileData.IsTaken.gameObject.TryGetComponent<EnergyCarrier>(out var component);

            if (component != null)
            {
                TryConnect(component);
            }

        }
        EventBus.Invoke<OnUpdateNetwork>();
    }


    public virtual void SetActive(bool isActive)
    {
        isHasEnergy = isActive;
        OnIsActive.Invoke(isHasEnergy);
    }

    protected void TryConnect(EnergyCarrier energyCarrier)
    {
        if ((energyCarrier.type & connectionsType) == 0) return; // 0 если нет совпадений по флагам

        connections.Add(energyCarrier);
        energyCarrier.connections.Add(this);

        OnConect.Invoke(energyCarrier.transform);
    }

    public virtual void TransferEnergy(EnergyTransferData transferData)
    {
        if (transferData.Visited.Contains(this)) return;
        if (transferData.Depth > transferData.MaxDepth) return;
        if (isHasEnergy) return;

        transferData.Visited.Add(this);
        SetActive(true);

        transferData.Depth += 1;
        foreach (var neighbor in connections)
        {
            neighbor.TransferEnergy(transferData);
        }
    }
}


public struct EnergyTransferData
{
    public HashSet<EnergyCarrier> Visited;
    public int Depth;
    public int MaxDepth;
    public Generator Generator;

    public EnergyTransferData(Generator newGenerator, int newMaxDepth)
    {
        Visited = new();
        Depth = 0;

        MaxDepth = newMaxDepth;
        Generator = newGenerator;

        Visited.Add(newGenerator);
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
