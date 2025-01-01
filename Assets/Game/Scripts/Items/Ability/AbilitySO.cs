using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "ScriptableObject/ability")]
public class AbilitySO : ScriptableObject
{
    public AbilityType abilityType;
    [Space]
    public TargetType targetType;
    public TriggerType triggerType;
    public EffectType effectType;
    [Space]
    public byte damage;
    public byte push;
    public byte reloadTime;
    public byte mannaCost;
    [Space]
    public MeshRenderer model;
    public AudioClip sound;
}

[Flags]
public enum AbilityType
{
    melee, range, magic, support, defense, heavy, splash
}

public enum TargetType
{
    yourself, first, second, last, all, previous, maxHp, lowHp,
}

public enum TriggerType
{
    none, newTarget, previousTarget, secondAttack,
}

public enum EffectType
{
    fire, poison, freeze, stun, heal, shield, pierce, weakness,
}
