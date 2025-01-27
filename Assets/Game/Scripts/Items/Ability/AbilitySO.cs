using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Other/Ability")]
public class AbilitySO : ItemSO
{
    [Space]
    public TargetSO targetStats;
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

//[System.Flags]
public enum DamageType
{
    physics, magic
}
