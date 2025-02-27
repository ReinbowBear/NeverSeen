using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName = "ScriptableObject/DataBase/ItemDB")]
public class ItemDataBase : ScriptableObject
{
    public ItemSO[] containers;

    public ItemSO GetItemByName(string name)
    {
        return Array.Find(containers, item => item.UI.itemName == name);
    }
}
