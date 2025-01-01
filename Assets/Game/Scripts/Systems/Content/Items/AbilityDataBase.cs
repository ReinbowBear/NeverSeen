using UnityEngine;

[CreateAssetMenu(fileName = "abilityDB", menuName = "ScriptableObject/abilityDB")]
public class AbilityDataBase : ScriptableObject
{
    public AbilityContainer[] containers;
}

[System.Serializable]
public class AbilityContainer : ItemContainer
{
    public AbilitySO stats;
}
