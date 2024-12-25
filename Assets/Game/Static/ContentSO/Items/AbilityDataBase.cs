using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDB", menuName = "ScriptableObject/AbilityDB")]
public class AbilityDataBase : ScriptableObject
{
    public AbilityContainer[] containers;
}

public class AbilityContainer : ItemContainer
{
    public AbilitySO stats;
}
