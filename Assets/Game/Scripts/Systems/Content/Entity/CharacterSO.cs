using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObject/Entity/Entity")]
public class CharacterSO : ScriptableObject
{
    public InterfaceSO UI;
    public bool isPlayer;
    [Space]
    public int health;
    public int armor;
    [Space]
    public float reloadMultiplier;
    [Space]
    public AbilitySO[] abilitys;
    [Space]
    public Mesh model;
}
