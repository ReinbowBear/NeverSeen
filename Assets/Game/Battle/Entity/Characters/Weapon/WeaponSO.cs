using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObject/Weapon")]
public class WeaponSO : ScriptableObject
{
    public enum DamageType
    {
        physical,
        magical
    }
    public DamageType damageType;
    public byte damage;
    public byte damageMultiplier;
    [Space]
    public byte reloadTime;
}
