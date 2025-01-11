
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
    Effect, Fire, Poison, Freeze, Stun, Heal, Shield, Crit
}