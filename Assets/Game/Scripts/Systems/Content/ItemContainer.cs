using UnityEngine;

[System.Serializable]
public class ItemContainer
{
    [HideInInspector] public byte containerID;
    public ItemType itemType;
    public InterfaceContainer UI;
}

public enum ItemType
{
    none,
    ability,
    equipment,
    armor,
}