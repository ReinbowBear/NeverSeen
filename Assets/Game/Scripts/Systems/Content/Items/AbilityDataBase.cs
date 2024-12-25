using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDB", menuName = "ScriptableObject/AbilityDB")]
public class AbilityDataBase : ScriptableObject
{
    public AbilityContainer[] containers;
}

[System.Serializable]
public class AbilityContainer : ItemContainer
{
    public AbilitySO stats;
}
