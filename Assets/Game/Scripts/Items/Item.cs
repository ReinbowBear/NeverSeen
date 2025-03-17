using UnityEngine;

public abstract class Item : MonoBehaviour
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
