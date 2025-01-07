using UnityEngine;

[CreateAssetMenu(fileName = "entityDB", menuName = "ScriptableObject/entityDB")]
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
