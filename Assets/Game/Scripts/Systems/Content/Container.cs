using UnityEngine.AddressableAssets;


public abstract class Container
{
    public AssetReference prefab;
    public byte id;
}


public abstract class ItemContainer : Container
{
    public enum ItemType
    {
        none,
        ability,
        equipment,
        armor,
    }
    public ItemType itemType;
    public InterfaceSO UI;
}
