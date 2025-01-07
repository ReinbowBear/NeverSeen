using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "ScriptableObject/ability")]
public class AbilitySO : ScriptableObject
{
    public AbilityType abilityType;
    [Space]
    public TargetType targetType;
    public TriggerType triggerType;
    public EffectType effectType;
    public EffectContainer effectContainer; //данные для эффектов
    [Space]
    public byte damage;
    public byte push;
    public byte reloadTime;
    public byte mannaCost;
    [Space]
    public MeshRenderer model;
    public AudioClip sound;
}

//[System.Flags]
public enum AbilityType
{
    melee, range, magic, support, defense, summons,
}

public enum TargetType
{
    BaseTarget, SecondTarget, LastTarget, AllTarget, _PreviousTarget, _MaxHpTarget, _LowHpTarget, _YourselfTarget
}

public enum TriggerType
{
    BaseTrigger, _NewTargetTrigger, _PreviousTargetTrigger, _SecondAttackTrigger,
}

public enum EffectType
{
    BaseEffect, _FireEffect, _PoisonEffect, _FreezeEffect, _StunEffect, _HealEffect, _ShieldEffect, _CritEffect
}
