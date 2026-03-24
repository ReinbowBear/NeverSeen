using UnityEngine;

[CreateAssetMenu(fileName = "BiomesAssetsSO", menuName = "Scriptable Objects/BiomesAssetsSO")]
public class BiomeAssetSO : ScriptableObject
{
    public GameObject prefab;
    public float ValueToSpawn;
    [Space]
    public NoisePipeline NoisePipeline;
    [Space]
    public SpawnCondition Condition;
}
