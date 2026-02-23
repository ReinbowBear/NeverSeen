using UnityEngine;

[CreateAssetMenu(fileName = "BiomeSO", menuName = "Scriptable Objects/BiomeSO")]
public class BiomeSO : ScriptableObject
{
    public BiomeType Type;
    public Color mapColor;

    public EnvironmentRule[] Environment;
    public EnvironmentRule[] Resources;
}

[System.Serializable]
public struct BiomeRule
{
    public int Count;
    public int Size;
}

[System.Serializable]
public struct EnvironmentRule
{
    public int MinCount;
    public int MaxCount;
}

public enum BiomeType
{
    Bottom, 
    Ground, 
    Hill, 
    Mount
}
