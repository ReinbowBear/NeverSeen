using System;
using UnityEngine;

[CreateAssetMenu(fileName = "abilityDB", menuName = "ScriptableObject/abilityDB")]
public class AbilityDataBase : ScriptableObject
{
    public AbilityContainer[] containers;

    public AbilityContainer GetItemByName(string name)
    {
        return Array.Find(containers, item => item.name == name);
    }
}

[System.Serializable]
public class AbilityContainer : ItemContainer
{
    public AbilitySO stats;
}
