using UnityEngine;

public abstract class ItemData : MonoBehaviour
{
    public InterfaceData UI;
    [Space]
    public ItemType itemType;
}


public enum ItemType
{
    None,
    Weapon,
    Ability,
    Armor,
    Movement
}
