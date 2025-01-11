using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Ability")]
public class AbilitySO : ScriptableObject
{
    public AbilityType abilityType;
    [Space]
    public TargetType targetType;
    public TriggerType triggerType;
    public EffectType effectType;
    public EffectSO effectStats;
    [Space]
    public byte damage;
    public int push;
    public byte reloadTime;
    public byte mannaCost;
    [Space]
    public MeshRenderer model;
    public AudioClip sound;
}
