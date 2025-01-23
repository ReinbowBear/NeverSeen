using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityDB", menuName = "ScriptableObject/DataBase/EntityDB")]
public class EntityDataBase : ScriptableObject
{
    public EntitySO[] containers;

    public EntitySO GetItemByName(string name)
    {
        return Array.Find(containers, item => item.UI.itemName == name);
    }
}
