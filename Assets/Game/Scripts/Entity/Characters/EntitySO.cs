using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObject/Other/Entity")]
public class EntitySO : ScriptableObject
{
    public InterfaceSO UI;
    public bool isPlayer;
    [Space]
    public int health;
    public int meleeArmor;
    public int rangeArmor;
    public int magicArmor;
    [Space]
    public float manna;
    public float mannaRegen;
    [Space]
    public string[] abilitys;
    [Space]
    public Mesh model;
}
