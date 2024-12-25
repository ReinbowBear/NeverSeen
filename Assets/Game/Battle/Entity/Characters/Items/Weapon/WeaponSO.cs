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
    public bool[] targets;

    public byte damage;
    public float damageScale;
    [Space]
    public byte reloadTime;
    public float reloadTimeScale;
}
