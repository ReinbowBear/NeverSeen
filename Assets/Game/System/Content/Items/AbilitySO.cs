using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Item/Ability")]
public class AbilitySO : ItemSO
{
    [Space]
    public EffectSO effect;
    [Space]
    public DamageType damageType;
    public byte damage;
    public byte reloadTime;
    [Space]
    public int push;
    public byte minPos;
    public byte maxPos;
    [Space]
    public Mesh model;
    public AudioClip sound;
}

[System.Flags]
public enum AbilityType
{
    physics, range, magic
}

public enum DamageType
{
    physics, magic
}
