using UnityEngine;

[CreateAssetMenu(fileName = "ArmorDB", menuName = "ScriptableObject/ArmorDB")]
public class ArmorDataBase : ScriptableObject
{
    public ArmorContainer[] containers;
}

[System.Serializable]
public class ArmorContainer : ItemContainer
{
    public byte test;
    //public ArmorSO stats;
}
