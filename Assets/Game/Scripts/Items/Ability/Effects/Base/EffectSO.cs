using UnityEngine;

[System.Serializable]
public class EffectSO
{
    public EffectType effectType;
    [Space]
    public string summon;
    public float value;
    public float duration;
    public bool isUpdate;
    [Space]
    public Sprite image;
    public AudioClip sound;
}

public enum EffectType
{
    Effect, Fire, Poison, Freeze, Stun, Heal, Shield
}
