using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigSO", menuName = "Scriptable Objects/ConfigSO")]
public class ConfigSO : ScriptableObject
{
    public EntityStats stats;

    [SerializeReference, HideInInspector] // едиторский скрипт отображает скписок (он не является тем же самым списком! но вносит изменения сюда)
    private List<IConfig> behaviorConfigs = new();

    public List<IBehavior> GetBehaviors()
    {
        List<IBehavior> behaviorsList = new();

        foreach (var serializeClass in behaviorConfigs)
        {
            behaviorsList.Add(serializeClass.Build());
        }
        return behaviorsList;
    }
}

[System.Serializable]
public struct EntityStats
{
    public string Name;
    public AudioClip SpawnSound;
    public AudioClip DestroySound;
    [Space]
    public int Cost;
    public int Radius;
    public ShapeType Shape;
}
