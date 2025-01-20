using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Ability")]
public class AbilitySO : ScriptableObject
{
    public DamageType damageType;
    [Space]
    public TargetType targetType;
    public TriggerType triggerType;
    public EffectType effectType;
    [Space]
    public byte damage;
    public int push;
    public byte reloadTime;
    public byte mannaCost;
    [Space]
    public Mesh model;
    public AudioClip sound;
    [Space]
    public EffectSO effectStats;
}
