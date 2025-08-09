using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnergyCarrier : BuildingAction
{
    public EnergyCarrierType Type;
    public EnergyCarrierType Connections;
    [Space]
    [SerializeField] private GameObject WirePref;
    public Transform WirePoint;
    public GameObject EnergyPoint;

    public Dictionary<EnergyCarrier, Wire> ConnectionsList = new();

    public override void Init()
    {
        foreach (var tile in Owner.TilesInRadius)
        {
            if (tile.tileData.IsTaken is Building building)
            {

                foreach (var component in building.PassiveActions)
                {
                    if (component is EnergyCarrier carrier)
                    {
                        SetWire(carrier);
                    }
                }
            }
        }
        ElectoNetwork.Instance.UpdateNetwork();
    }


    public override void Active(bool isActive)
    {
        EnergyPoint.SetActive(isActive);
    }

    public override void Delete()
    {
        foreach (var key in ConnectionsList.Keys)
        {
            Destroy(ConnectionsList[key].gameObject);
            key.ConnectionsList.Remove(this);
        }
        ElectoNetwork.Instance.RemoveEnergyCarrier(this);
        ElectoNetwork.Instance.UpdateNetwork();
    }

    public virtual void TransferEnergy(EnergyData energyData)
    {
        if (energyData.Visited.Contains(this)) return;
        if (energyData.Depth > energyData.MaxDepth) return;

        energyData.Visited.Add(this);
        Active(true);

        energyData.Depth += 1;
        foreach (var neighborKey in ConnectionsList.Keys)
        {
            neighborKey.TransferEnergy(energyData);
        }
    }

    protected void SetWire(EnergyCarrier energyCarrier)
    {
        if ((energyCarrier.Type & Connections) == 0) return; // 0 если нет совпадений по флагам

        GameObject obj = Instantiate(WirePref, WirePoint);
        Wire component = obj.GetComponent<Wire>();

        component.StartPoint = WirePoint.position;
        component.EndPoint = energyCarrier.WirePoint.position;
        component.SetLine();

        ConnectionsList.Add(energyCarrier, component);
        energyCarrier.ConnectionsList.Add(this, component);
    }
}


[Flags]
public enum EnergyCarrierType
{
    None = 0, // 000
    Generator = 1 << 0, // 1 — 001
    Tower = 1 << 1, // 2 — 010
    User = 1 << 2  // 4 — 100
}