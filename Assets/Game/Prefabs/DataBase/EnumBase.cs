
//[System.Flags]
public enum DamageType
{
    melee, range, magic
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
    Effect, Fire, Poison, Freeze, Stun, Heal, Shield, Crit
}