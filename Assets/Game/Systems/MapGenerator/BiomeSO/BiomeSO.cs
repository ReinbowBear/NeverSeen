using UnityEngine;

[CreateAssetMenu(fileName = "BiomeSO", menuName = "Scriptable Objects/BiomeSO")]
public class BiomeSO : ScriptableObject
{
    [Header("General")]
    public BiomeType Type;
    public Color Color;
    [Space]
    public NoisePipeline NoisePipeline;
    [Space]
    public SpawnCondition Condition;
    [Space]
    [Header("Environment")]
    public BiomeAssetSO[] Assets;
}

public enum BiomeType
{
    DeepWater, 
    Water,
    Grass,
    Snow, 
    Mountain
}

