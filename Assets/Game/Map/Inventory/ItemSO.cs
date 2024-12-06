using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        enemy,
        
        character,
        ability,
        equip
    }

    public AssetReference itemPrefab;
    public CharacterStatsSO characterStatsSO; //может быть нулём так как не всегда тип предмета персонаж
    public ItemType itemType;
    [Space]
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}
