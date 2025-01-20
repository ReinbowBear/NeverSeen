using UnityEngine;

[CreateAssetMenu(fileName = "WaveDB", menuName = "ScriptableObject/DBWave")]
public class WavesDataBase : ScriptableObject
{
    public WaveContainer[] containers;
}

[System.Serializable]
public class WaveContainer
{
    public string[] enemys;
}
