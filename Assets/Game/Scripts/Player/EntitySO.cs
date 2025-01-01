using UnityEngine;

[CreateAssetMenu(fileName = "newEntity", menuName = "ScriptableObject/entity")]
public class EntitySO : ScriptableObject
{
    public float health;
    public byte armor;
    [Space]
    public string[] abilitys;
    [Space]
    public byte manna;
    public float mannaRegen;
    [Space]
    public MeshRenderer model;
}
