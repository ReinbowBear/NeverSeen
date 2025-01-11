using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDB", menuName = "ScriptableObject/DBability")]
public class AbilityDataBase : ScriptableObject
{
    public AbilityContainer[] containers;

    public AbilityContainer GetItemByName(string name)
    {
        return Array.Find(containers, item => item.UI.itemName == name);
    }
}

[System.Serializable]
public class AbilityContainer : ItemContainer
{
    public AbilitySO stats;
}
