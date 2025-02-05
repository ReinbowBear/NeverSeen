using UnityEngine;

[CreateAssetMenu(fileName = "WaveDB", menuName = "ScriptableObject/DataBase/WaveDB")]
public class WavesDataBase : ScriptableObject
{
    public WaveContainer[] containers;
}

[System.Serializable]
public class WaveContainer
{
    public string[] enemys;
}
