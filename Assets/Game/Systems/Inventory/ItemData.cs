using UnityEngine;

public struct ItemData
{
    public Sprite Sprite;
    public string ItemName;
    public ItemType ItemType;
    [Space]
    [TextArea(5, 0)]
    public string Description;
}

public enum ItemType
{
    None,
    Weapon,
    Defense,
    Movement
}
