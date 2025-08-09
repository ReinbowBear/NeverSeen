using UnityEngine;

public abstract class ItemData : MonoBehaviour
{
    public InterfaceData UI;
    public ItemType itemType;
}


[System.Serializable]
public class InterfaceData
{
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}


public enum ItemType
{
    None,
    Weapon,
    Defense,
    Movement
}