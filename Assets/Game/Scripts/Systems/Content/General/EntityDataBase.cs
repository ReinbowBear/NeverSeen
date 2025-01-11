using UnityEngine;

[CreateAssetMenu(fileName = "EntityDB", menuName = "ScriptableObject/DBentity")]
public class EntityDataBase : ScriptableObject
{
    public EntityContainer[] containers;
}

[System.Serializable]
public class EntityContainer
{
    public EntitySO stats;
    public InterfaceContainer UI;
}
