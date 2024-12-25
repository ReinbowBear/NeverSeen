using UnityEngine;

[CreateAssetMenu(fileName = "MapsDB", menuName = "ScriptableObject/MapsDB")]
public class MapDataBase : ScriptableObject
{
    public MapContainer[] containers;
}


[System.Serializable]
public class MapContainer : Container
{

}
