using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class CharacterSO : ScriptableObject
{
    public byte health;
    public byte armor;
    [Space]
    public byte manna;
    public float mannaMultiplier;
    [Space]
    public float damageMultiplier;
    public float takeDamageMultiplier;
}
