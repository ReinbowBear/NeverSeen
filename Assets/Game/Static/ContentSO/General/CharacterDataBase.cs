using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDB", menuName = "ScriptableObject/CharacterDB")]
public class CharacterDataBase : ScriptableObject
{
    public CharacterContainer[] containers;
}

[System.Serializable]
public class CharacterContainer : Container
{
    public CharacterSO stats;
    public InterfaceSO UI;
}
