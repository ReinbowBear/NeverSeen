using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "MapsDB", menuName = "ScriptableObject/DataBase/MapsDB")]
public class MapDataBase : ScriptableObject
{
    public AssetReference[] containers;
}
