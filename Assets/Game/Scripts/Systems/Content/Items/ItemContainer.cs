using UnityEngine;

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