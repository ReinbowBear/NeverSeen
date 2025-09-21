using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Entity : MonoBehaviour
{
    public Action<bool> OnSelected;

    [SerializeField] private ConfigSO ConfigSO;
    public EntityStats Stats { get; protected set; }
    public Dictionary<Type, object> Behaviours { get; protected set; } = new ();


    public List<Tile> TilesInRadius { get; protected set; } = new();
    public Tile Tile { get; protected set; }

    [Inject] private Factory factory;

    void Awake()
    {
        Stats = ConfigSO.Stats;
        
        foreach (var configClass in ConfigSO.BehaviorConfigs)
        {
            var type = configClass.GetType();
            var args = configClass.GetArgs();
            var myClass = (IBehavior)factory.GetClassWithActivator(type, args);
            
            Behaviours.Add(type, myClass);
        }
    }

    public void Init(Tile tile, List<Tile> list)
    {
        Tile = tile;
        TilesInRadius = list;
    }


    public void Selected(bool isSelected)
    {
        OnSelected?.Invoke(isSelected);
    }


    public T GetBehaviour<T>() where T : class
    {
        if (Behaviours.TryGetValue(typeof(T), out var EntityClass))
        {
            return (T)EntityClass;
        }

        return null;
    }


    protected virtual void OnDelete()
    {
        Selected(false);
    }

    void OnDisable()
    {
        Selected(false);
    }
}
