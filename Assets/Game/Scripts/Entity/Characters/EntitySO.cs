using UnityEngine;

[CreateAssetMenu(fileName = "newEntity", menuName = "ScriptableObject/entity")]
public class EntitySO : ScriptableObject
{
    public bool isPlayer;
    [Space]
    public float health;
    public int meleeArmor;
    public int rangeArmor;
    public int magicArmor;
    [Space]
    public string[] abilitys;
    [Space]
    public float manna;
    public float mannaRegen;
    [Space]
    public Mesh model;
}
