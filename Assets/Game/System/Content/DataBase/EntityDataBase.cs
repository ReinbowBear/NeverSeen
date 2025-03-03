using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityDB", menuName = "ScriptableObject/DataBase/EntityDB")]
public class EntityDataBase : ScriptableObject
{
    public CharacterSO[] containers;

    public CharacterSO GetItemByName(string name)
    {
        return Array.Find(containers, item => item.UI.itemName == name);
    }
}
