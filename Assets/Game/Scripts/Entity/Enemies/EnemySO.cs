using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    public float health;
    public byte armor;
    [Space]
    public byte reloadTime;
    public float reloadTimeScale;
    [Space]
    public float damageScale;
    public float takeDamageScale;
}
