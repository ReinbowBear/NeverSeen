using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "ScriptableObject/Ability")]
public class AbilitySO : ScriptableObject
{
    [Flags]
    public enum AbilityType
    {
        melee,
        range,
        magic,
        support,
        defense,
        heavy,
        splash
    }
    public AbilityType abilityType;
    
    public byte damage;
    public byte damageMultiplier;
    [Space]
    public byte reloadTime;
}
