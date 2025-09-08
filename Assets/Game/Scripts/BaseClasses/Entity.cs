using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Action<bool> OnSelected;

    [SerializeField] private ConfigSO ConfigSO;
    public EntityStats Stats { get; protected set; }
    public List<IBehavior> Behaviours { get; protected set; }

    public List<Tile> TilesInRadius { get; protected set; } = new();
    public Tile Tile { get; protected set; }

    void Awake()
    {
        Stats = ConfigSO.stats;
        Behaviours = ConfigSO.GetBehaviors();
    }

    public void Init(Tile tile, List<Tile> list)
    {
        Tile = tile;
        TilesInRadius = list;
    }


    public void Selected(bool isSelected)
    {
        foreach (var tile in TilesInRadius)
        {
            tile.SetBacklight(isSelected);
        }

        OnSelected?.Invoke(isSelected);

        if (!isSelected) return;

        Tween.Impact(transform);
    }

    protected virtual void OnDelete()
    {
        Selected(false);
    }


    void OnDisable()
    {
        OnDelete();
    }
}
