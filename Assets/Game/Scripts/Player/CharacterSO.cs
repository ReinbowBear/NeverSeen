using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class CharacterSO : ScriptableObject
{
    public float health;
    public byte armor;
    [Space]
    public byte manna;
    public float mannaScale;
    [Space]
    public float damageScale;
    public float takeDamageScale;
}
