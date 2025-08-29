using System.Collections.Generic;
using UnityEngine;

public class ConfigHandler : Saveable
{
    public ConfigSO ConfigSO;

    public EntityStats Stats { get; private set; }
    public List<IBehavior> Behaviours { get; private set; }

    void Awake()
    {
        Stats = ConfigSO.stats;
        Behaviours = ConfigSO.GetBehaviors();
    }


    protected override void OnSave(OnSave _)
    {
        SaveData(Stats);
    }

    protected override void OnLoad(OnLoad _)
    {
        Stats = (EntityStats)LoadData<EntityStats>();
    }
}
