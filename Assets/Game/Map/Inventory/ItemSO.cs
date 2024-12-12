using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        none,
        weapon,
        ability,
        equip
    }
    public ItemType itemType;
}
