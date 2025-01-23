using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDB", menuName = "ScriptableObject/DataBase/AbilityDB")]
public class AbilityDataBase : ScriptableObject
{
    public AbilitySO[] containers;

    public AbilitySO GetItemByName(string name)
    {
        return Array.Find(containers, item => item.UI.itemName == name);
    }
}
