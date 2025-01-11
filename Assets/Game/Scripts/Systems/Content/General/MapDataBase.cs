using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "MapsDB", menuName = "ScriptableObject/DBmaps")]
public class MapDataBase : ScriptableObject
{
    public MapContainer[] containers;
}


[System.Serializable]
public class MapContainer
{
    public AssetReference prefab;
}
