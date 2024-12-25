using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDB", menuName = "ScriptableObject/EnemyDB")]
public class EnemyDataBase : ScriptableObject
{
    public EnemyContainer[] containers;
}

[System.Serializable]
public class EnemyContainer : Container
{
    public CharacterSO stats;
    public InterfaceSO UI;
}
