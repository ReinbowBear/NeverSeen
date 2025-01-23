using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityDB", menuName = "ScriptableObject/DataBase/EntityDB")]
public class EntityDataBase : ScriptableObject
{
    public EntityContainer[] containers;

    public EntityContainer GetItemByName(string name)
    {
        return Array.Find(containers, item => item.UI.itemName == name);
    }
}

[System.Serializable]
public class EntityContainer
{
    public EntitySO stats;
    public InterfaceSO UI;
}
