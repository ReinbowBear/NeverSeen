using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "mapsDB", menuName = "ScriptableObject/mapsDB")]
public class MapDataBase : ScriptableObject
{
    public MapContainer[] containers;
}


[System.Serializable]
public class MapContainer
{
    public AssetReference prefab;
}
