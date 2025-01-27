using UnityEngine;

[System.Serializable]
public class TargetSO
{
    public TargetType targetType;
    public bool isEntitySide;
    [Space]
    public byte midDist;
    public byte maxDist;
}

public enum TargetType
{
    BaseTarget, AllTarget, FromPosTarget, _MaxHpTarget, _LowHpTarget,
}
