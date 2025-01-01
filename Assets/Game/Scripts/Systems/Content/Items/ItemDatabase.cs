using System;
using UnityEngine;

[CreateAssetMenu(fileName = "itemDB", menuName = "ScriptableObject/itemDB")]
public class ItemDatabase : ScriptableObject
{
    public ItemContainer[] containers;

    public ItemContainer GetItemByName(string name)
    {
        return Array.Find(containers, item => item.name == name);
    }
}

[System.Serializable]
public class ItemContainer
{
    [HideInInspector] public byte containerID;
    public ItemType itemType;
    [Space]
    public Sprite sprite;
    public string name;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

public enum ItemType
{
    none,
    ability,
    equipment,
    armor,
}