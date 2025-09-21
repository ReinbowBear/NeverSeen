using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigSO", menuName = "Scriptable Objects/ConfigSO")]
public class ConfigSO : ScriptableObject
{
    public EntityStats Stats;

    [SerializeReference, HideInInspector]
    public List<IConfig> BehaviorConfigs = new();
}

[System.Serializable]
public struct EntityStats
{
    public string Name;
    public AudioSO audio;
    [Space]
    public int Cost;
    public int Radius;
    public ShapeType Shape;
}
