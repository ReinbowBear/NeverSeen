using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Other/Ability")]
public class AbilitySO : ItemSO
{
    [Space]
    public TargetType targetType;
    public TriggerType triggerType;
    public EffectType effectType;
    public EffectSO effectStats;
    [Space]
    public DamageType damageType;
    public byte damage;
    public int push;
    public byte reloadTime;
    public byte mannaCost;
    [Space]
    public Mesh model;
    public AudioClip sound;
}
