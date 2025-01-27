using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObject/Other/Entity")]
public class EntitySO : ScriptableObject
{
    public InterfaceSO UI;
    public bool isPlayer;
    [Space]
    public int health;
    public int armor;
    [Space]
    public float manna;
    public float mannaRegen;
    [Space]
    public AbilitySO[] abilitys;
    [Space]
    public Mesh model;
}
